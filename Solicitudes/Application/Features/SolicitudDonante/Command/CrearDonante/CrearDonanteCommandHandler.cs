using System.Collections;
using MassTransit;
using MassTransitMessages.Messages;
using MediatR;
using Solicitudes.Domain.Entities;
using Solicitudes.Infrastructure.Repositories.SpecificationUnitOfWork;

namespace Solicitudes.Application.Features.SolicitudDonante.Command.CrearDonante;

public class CrearDonanteCommandHandler : IRequestHandler<CrearDonanteCommand, CrearDonanteResponse>
{
    private readonly ISolicitudesSpecificationUnitOfWork _solicitudesSpecificationUnitOfWork;
    private readonly IBus _massTransitService;

    public CrearDonanteCommandHandler(ISolicitudesSpecificationUnitOfWork solicitudesSpecificationUnitOfWork, IBus massTransitService)
    {
        _solicitudesSpecificationUnitOfWork = solicitudesSpecificationUnitOfWork;
        _massTransitService = massTransitService;
    }

    public async Task<CrearDonanteResponse> Handle(CrearDonanteCommand request, CancellationToken cancellationToken)
    {
        var solicitudUsuario = await CrearSolicitud(request);
        var archivosInfo = await RetornarArchivos(request);
        var informacionCorreos=await CrearCorreosAdministradores(solicitudUsuario);
        SolicitudCrearDonanteMessage message = new(){
            CorrelationId = NewId.NextSequentialGuid(),
            SolicitudUsuarioId = solicitudUsuario.SolicitudUsuarioId,
            SagaQueueName = "saga-crear-donante",
            Archivos = archivosInfo,
            InformacionCorreo = informacionCorreos,
        };
        var endpoint = await _massTransitService.GetSendEndpoint(new Uri($"queue:saga-crear-donante"));
        await endpoint.Send(message);
        return new CrearDonanteResponse {SolicitudId=solicitudUsuario.SolicitudUsuarioId};
    }
    public async Task<SolicitudUsuario> CrearSolicitud(CrearDonanteCommand request){
        var solicitud = new SolicitudUsuario();
        solicitud = solicitud.CrearSolicitud(request.NumeroDocumento,request.FechaExpedicionDocumento,request.PrimerApellido,
        request.PrimerNombre,request.SegundoApellido,request.SegundoNombre,request.CorreoElectronico,1,request.TipoCargoPersonaId,request.Celular,
        request.Direccion,request.MunicipioDireccionId);
        await _solicitudesSpecificationUnitOfWork._solicitudRepository.AddAsync(solicitud);
        await _solicitudesSpecificationUnitOfWork.SaveChangesAsync();
        return solicitud;
         
    }
     private async Task<List<ArchivoDtoInfo>> RetornarArchivos(CrearDonanteCommand request)
    {
        // Crear una lista para almacenar la información de cada archivo
        List<ArchivoDtoInfo> archivosInfo = new List<ArchivoDtoInfo>();
        // Recorrer cada objeto CrearArchivoDto y obtener la información necesaria de cada archivo
        foreach (var archivoDto in request.Archivos)
        {
            byte[] archivoBytes;
            string nombreArchivo;
            string tipoArchivo;
            using (var memoryStream = new MemoryStream())
            {
                await archivoDto.Archivo.CopyToAsync(memoryStream);
                archivoBytes = memoryStream.ToArray();
            }
            nombreArchivo = archivoDto.Archivo.FileName;
            tipoArchivo = archivoDto.Archivo.ContentType;
            // Crear un objeto ArchivoDtoInfo con la información del archivo
            ArchivoDtoInfo archivoInfo = new ArchivoDtoInfo
            {
                ArchivoBytes = archivoBytes,
                NombreArchivo = nombreArchivo,
                TipoArchivo = tipoArchivo,
                TipoArchivoId = int.Parse(archivoDto.TipoArchivoId),
            };
            // Agregar el objeto ArchivoDtoInfo a la lista
            archivosInfo.Add(archivoInfo);
        }
        return archivosInfo;
    }
    public async Task<List<EnviarCorreoDto>> CrearCorreosAdministradores(SolicitudUsuario detalle)
    {
        var listaCorreoDto = new List<EnviarCorreoDto>();
        var listaAdministradores = new List<string> { "adminmicroservicios2024@example.com", "adminmicroservicios2025@example.com" };

        var nombreCompletoPersonaContacto = $"{detalle.PersonaPrimerNombre} {detalle.PersonaSegundoNombre} {detalle.PersonaPrimerApellido} {detalle.PersonaSegundoApellido}";
        Hashtable dataAdmin = new()
        {
           { "ROL",  "Administrador"},
           { "NOMBRE", nombreCompletoPersonaContacto},
           { "DOCUMENTO", detalle.PersonaNumeroDocumento == null ? "" : detalle.PersonaNumeroDocumento },
           { "CORREOELECTRONICO", detalle.PersonaCorreoElectronico == null ? "" : detalle.PersonaCorreoElectronico }
        };
        var correoAdmin = new EnviarCorreoDto
        {
            nombreTemplate = "solicitudAdmin.html",
            nombreTemplateError = "solicitudError.html",
            recipientes = listaAdministradores,
            informacionCorreo = dataAdmin
        };
        listaCorreoDto.Add(correoAdmin);
        return listaCorreoDto;
    }
    
}
