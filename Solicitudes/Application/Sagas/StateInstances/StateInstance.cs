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
