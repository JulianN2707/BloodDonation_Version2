using System;
using System.Collections;

namespace MassTransitMessages.Messages;

public class MassTransitMessages
{

}
public class CrearPersonaMessage{
    public Guid CorrelationId { get; set; }
    public Guid SolicitudUsuarioId { get; set; }
    public string NumeroDocumento { get; set; } = null!;
    public string? PrimerApellido { get; set; }
    public string? PrimerNombre { get; set; }
    public string? SegundoApellido { get; set; }
    public string? SegundoNombre { get; set; }
    public string? CorreoElectronico { get; set; }
    public string? Celular { get; set; }
    public string? Direccion { get; set; }
    public int? MunicipioDireccionId { get; set; }
    public string SagaQueueName { get; set; } = string.Empty;
}
public class RollbackCrearPersonaMessage 
{
    public Guid CorrelationId { get; set; }
    public int PersonaId { get; set; }
    public Guid SolicitudUsuarioId { get; set; }
}
public class SolicitudCrearDonanteMessage
{
    public Guid CorrelationId { get; set; }
    public Guid SolicitudUsuarioId { get; set; }
    public List<ArchivoDtoInfo> Archivos { get; set; }
    public List<EnviarCorreoDto> InformacionCorreo { get; set; }
    public string SagaQueueName { get; set; }
}
public class ArchivosCreadosExitosamenteEvent
{
    public Guid CorrelationId { get; set; }
    public Guid SolicitudUsuarioId { get; set; }
}
public class ArchivosCreadosErrorEvent 
{
    public Guid CorrelationId { get; set; }
    public Guid SolicitudUsuarioId { get; set; }
}
public class RollbackEliminarSolicitudEvent
{
    public Guid CorrelationId { get; set; }
    public Guid SolicitudUsuarioId { get; set; }
}
public class NotificacionExitosaEvent 
{
    public Guid CorrelationId { get; set; }
    public Guid SolicitudUsuarioId { get; set; }
}
public class EnviarListaArchivos 
{
    public Guid CorrelationId { get; set; }
    public Guid SolicitudUsuarioId { get; set; }
    public List<ArchivoDtoInfo> Archivos { get; set; }
    public string SagaQueueName { get; set; }
}
public class RollBackCrearSolicitudEvent 
{
    public Guid CorrelationId { get; set; }
    public Guid SolicitudUsuarioId { get; set; }
    public string SagaQueueName { get; set; }
}
public class EnviarNotificacionMessage
{
    public Guid CorrelationId { get; set; }
    public Guid SolicitudUsuarioId { get; set; }
    public bool Estado { get; set; }
    public List<EnviarCorreoDto> InformacionCorreo { get; set; }
    public string SagaQueueName { get; set; }
}
public class RollBackDesaprobarSolicitudEvent 
{
    public Guid CorrelationId { get; set; }
    public Guid SolicitudUsuarioId { get; set; }
}
public record struct ArchivoDtoInfo
{
    public byte[] ArchivoBytes { get; init; }
    public string NombreArchivo { get; init; }
    public string TipoArchivo { get; init; }
    public int TipoArchivoId { get; init; }
}
public class EnviarCorreoDto
{
    public Hashtable informacionCorreo {  get; set; }
    public string nombreTemplate { get; set; }
    public string nombreTemplateError { get; set; }
    public List<string> recipientes { get; set; }

}
public class RollBackCrearSolicitudEjecutadoEvent
{
    public Guid CorrelationId { get; set; }
    public Guid SolicitudUsuarioId { get; set; }
}
public class ObtenerInformacionSolicitudMessage
{
    public Guid CorrelationId { get; set; }
    public Guid SolicitudUsuarioId { get; set; }
    public string SagaQueueName { get; set; }

}

