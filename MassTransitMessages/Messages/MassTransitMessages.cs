using System;

namespace MassTransitMessages.Messages;

public class MassTransitMessages
{

}
public class CrearPersonaMessage{
    public Guid CorrelationId { get; set; }
    public int SolicitudUsuarioId { get; set; }
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
    public int SolicitudUsuarioId { get; set; }
}
