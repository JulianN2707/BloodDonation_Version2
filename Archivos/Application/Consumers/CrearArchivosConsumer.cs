using System;
using System.Text.RegularExpressions;
using Archivos.Domain.Entities;
using Archivos.Infrastructure.Context;
using MassTransit;
using MassTransitMessages.Messages;

namespace Archivos.Application.Consumers;

public class CrearArchivosConsumer : IConsumer<EnviarListaArchivos>
{
    private readonly ArchivosContext _context;
    private readonly ILogger<CrearArchivosConsumer> _logger;

    public CrearArchivosConsumer(
        ArchivosContext archivosRepository, ILogger<CrearArchivosConsumer> logger)
    {
        _context = archivosRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<EnviarListaArchivos> context)
    {
        var data = context.Message;
        var falloArchivo = false;
        foreach (var item in data.Archivos)
        {
            var respuesta = await CrearArchivoAsync(item);
            if (respuesta == false)
            {
                falloArchivo = true;
            }
        }
        if (falloArchivo)
        {
            var crearArchivosETErrorEvent = new ArchivosCreadosErrorEvent
            {
                CorrelationId = context.Message.CorrelationId,
                SolicitudUsuarioId = context.Message.SolicitudUsuarioId,
            };
            var endpoint = await context.GetSendEndpoint(new Uri($"queue:{data.SagaQueueName}"));
            await endpoint.Send(crearArchivosETErrorEvent);
            _logger.LogInformation($"BUS:crearArchivosErrorEvent event consumed. Message: {context.Message.CorrelationId}");
        }
        else
        {
            await _context.SaveChangesAsync();
            var crearArchivosEvent = new ArchivosCreadosExitosamenteEvent
            {
                CorrelationId = context.Message.CorrelationId,
                SolicitudUsuarioId = context.Message.SolicitudUsuarioId,
            };
            // Publica el evento en la cola directa
            var endpoint = await context.GetSendEndpoint(new Uri($"queue:{data.SagaQueueName}"));
            await endpoint.Send(crearArchivosEvent);
            _logger.LogInformation($"BUS:crearArchivosEvent event consumed. Message: {context.Message.CorrelationId}");
        }
    }

    public async Task<bool> CrearArchivoAsync(ArchivoDtoInfo archivoInfo)
    {
        //valido que envie un archivo
        if (archivoInfo.ArchivoBytes.Length == 0)
        {
            _logger.LogInformation($"crearArchivosEntidadTerritorialEvent event consumed. Message:Uno o más archivos tiene un tamaño de 0");
            return false;
        }
        //valido que el archivo no pese mas de 5 MB
        bool isTrue = ValidarTamanoArchivo(archivoInfo.ArchivoBytes);
        if (isTrue)
        {
            _logger.LogInformation($"crearArchivosEntidadTerritorialEvent event consumed. Message:El archivo pesa {archivoInfo.ArchivoBytes.Length} no puede superar los 5MB.");
            return false;
        }
        string fullFilePath = string.Empty;
        var tamanio = Convert.ToSingle(ConvertBytesToMegabytes(archivoInfo.ArchivoBytes.Length));
        var newFileName = Regex.Replace($"{Guid.NewGuid()}-{archivoInfo.NombreArchivo}", @"\s+", "");
        var localPath = "/home/julian-andres-nino/Escritorio/Uploads";
        var useLocal = true;
        string filePath = $"{localPath}";

        #region Save in File System
        if (useLocal)
        {
            if (OperatingSystem.IsLinux())
            {
                filePath = "/home/julian-andres-nino/Escritorio/Uploads";
            }
            byte[] fileData = archivoInfo.ArchivoBytes;

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            if (OperatingSystem.IsLinux())
            {
                fullFilePath = $"{filePath}/{newFileName}";
            }
            else
            {
                fullFilePath = $"{filePath}\\{newFileName}";
            }

            Stream fileStream = new FileStream(fullFilePath, FileMode.Create);
            await fileStream.WriteAsync(fileData);
            fileStream.Close();
        }
        #endregion

        var archivo = await CreateArchivoSolicitud(archivoInfo, archivoInfo.NombreArchivo, fullFilePath, tamanio);
        if (archivo is not null)
        {
            _logger.LogInformation($"Archivo creado correctamente con nombre: {newFileName}");
            return true;
        }
        else
        {
            _logger.LogInformation($"Archivo no se ha creado correctamente");
            return false;
        }
    }

    public async Task<Archivo?> CreateArchivoSolicitud(ArchivoDtoInfo archivoInfo, string fileName, string url, float tamanio, DateTime? fechaActualizacion = null)
    {
        try
        {
            var extension = Path.GetExtension(fileName);
            Archivo archivo = new Archivo
                {Nombre=fileName.ToLowerInvariant(),
                Ruta=url,
                TipoArchivoId=archivoInfo.TipoArchivoId,
                Extension=extension,
                FechaCreacion=DateTime.UtcNow};

            _context.Archivo.Add(archivo);
            return archivo;
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"Archivo no se ha creado correctamente {ex}");
            return null;
        }
    }
    private bool ValidarTamanoArchivo(byte[] archivoBytes)
    {
        double tamanoEnMB = ConvertBytesToMegabytes(archivoBytes.Length); // Convertir a MB

        if (tamanoEnMB > 5)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private double ConvertBytesToMegabytes(long bytes)
    {
        return bytes / (1024f * 1024f);
    }
}


