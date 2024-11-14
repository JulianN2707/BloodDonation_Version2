using System;
using MassTransit;
using MassTransitMessages.Messages;

namespace Solicitudes.Application.Sagas.StateInstances;

public class SolicitudCrearDonanteStateInstance : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public Guid SolicitudUsuarioId { get; set; }
    public List<ArchivoDtoInfo> Archivos { get; set; }
    public List<EnviarCorreoDto> InformacionCorreo { get; set; }
    public bool Estado { get; set; }
    public string CurrentState { get; set; }
    public DateTime DateTimeState { get; set; }
    public string SagaQueueName { get; set; }
}
public class SolicitudAprobarDonanteStateInstance : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public Guid SolicitudUsuarioId { get; set; }
    public Guid UsuarioId { get; set; }
    public Guid PersonaId { get; set; }
    public string? PersonaDireccion { get; set; }
    public string? PersonaNumeroCelular { get; set; }
    public string? PersonaCorreoElectronico { get; set; }
    public string? PersonaPrimerNombre { get; set; }
    public string? PersonaPrimerApellido { get; set; }
    public List<CargaArchivoDto>? ArchivosCurador { get; set; }
    public string Rol { get; set; }
    public List<EnviarCorreoDto> InformacionCorreo { get; set; }
    public bool Estado { get; set; }
    public string CurrentState { get; set; }
    public DateTime DateTimeState { get; set; }
    public string SagaQueueName { get; set; }
}
