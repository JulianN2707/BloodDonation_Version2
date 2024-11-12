using System;
using MassTransit;
using MassTransitMessages.Messages;

namespace Solicitudes.Application.Sagas.StateInstances;

public class SolicitudDonanteCreadoStateMachine : MassTransitStateMachine<SolicitudCrearDonanteStateInstance>
{
    //Evento Inicial
    public Event<SolicitudCrearDonanteMessage> EnviarCreacionDonanteMessage { get; set; }
    //
    public Event<ArchivosCreadosExitosamenteEvent> ArchivosCreadosEvent { get; set; }
    public Event<ArchivosCreadosErrorEvent> ArchivosCreadosErrorEvent { get; set; }
    public Event<RollbackEliminarSolicitudEvent> RollbackEjecutadoEvent { get; set; }
    public Event<NotificacionExitosaEvent> NotificacionExitosaEvent { get; set; }
    //States 
    public State SolicitudDonanteCreada { get; private set; }
    public State ArchivosCreados { get; private set; }
    public State ArchivosCreadosError { get; private set; }
    public State RollBackEjecutado { get; private set; }
    public State NotificacionEnviada { get; private set; }
    public State NotificacionExitosa { get; private set; }

    public SolicitudDonanteCreadoStateMachine(){
        InstanceState(x => x.CurrentState);

        Event(() => EnviarCreacionDonanteMessage, x => x.CorrelateById(y => y.Message.CorrelationId));
        Event(() => ArchivosCreadosEvent, x => x.CorrelateById(y => y.Message.CorrelationId));
        Event(() => ArchivosCreadosErrorEvent, x => x.CorrelateById(y => y.Message.CorrelationId));
        Event(() => RollbackEjecutadoEvent, x => x.CorrelateById(y => y.Message.CorrelationId));
        Event(() => NotificacionExitosaEvent, x => x.CorrelateById(y => y.Message.CorrelationId));

        Initially(When(EnviarCreacionDonanteMessage)
            .Then(context =>
            {
                context.Saga.SolicitudUsuarioId = context.Message.SolicitudUsuarioId;
                context.Saga.DateTimeState = DateTime.UtcNow;
                context.Saga.Archivos = context.Message.Archivos;
                context.Saga.InformacionCorreo = context.Message.InformacionCorreo;
                context.Saga.SagaQueueName = context.Message.SagaQueueName;
            }).TransitionTo(SolicitudDonanteCreada)
            .Send(new Uri($"queue:crear-archivos"), context => new EnviarListaArchivos
            {
                CorrelationId = context.Saga.CorrelationId,
                SolicitudUsuarioId = context.Saga.SolicitudUsuarioId,
                Archivos = context.Saga.Archivos,
                SagaQueueName=context.Saga.SagaQueueName,
            }));

        During(SolicitudDonanteCreada,
        When(ArchivosCreadosErrorEvent)
        .Then(context =>
        {
            context.Saga.SolicitudUsuarioId = context.Message.SolicitudUsuarioId;
            context.Saga.DateTimeState = DateTime.UtcNow;
        }).TransitionTo(ArchivosCreadosError)
        .Send(new Uri($"queue:eliminar-solicitud"), context => new RollBackCrearSolicitudEvent
        {
            CorrelationId = context.Saga.CorrelationId,
            SolicitudUsuarioId = context.Saga.SolicitudUsuarioId,
            SagaQueueName= context.Saga.SagaQueueName,
        }));

        During(SolicitudDonanteCreada,
           When(ArchivosCreadosEvent)
           .Then(context =>
           {
               context.Saga.DateTimeState = DateTime.UtcNow;
               context.Saga.SolicitudUsuarioId = context.Message.SolicitudUsuarioId;
               context.Saga.Estado = true; // verdadero porque termino correctamente
           }).TransitionTo(ArchivosCreados).Send(new Uri($"queue:enviar-notificacion"), context => new EnviarNotificacionMessage
           {
               CorrelationId = context.Saga.CorrelationId,
               SolicitudUsuarioId = context.Saga.SolicitudUsuarioId,
               Estado = context.Saga.Estado,
               InformacionCorreo = context.Saga.InformacionCorreo,
               SagaQueueName= context.Saga.SagaQueueName,
           }).TransitionTo(NotificacionEnviada));

        During(ArchivosCreadosError,
        When(RollbackEjecutadoEvent)
        .Then(context =>
        {
            context.Saga.SolicitudUsuarioId = context.Message.SolicitudUsuarioId;
            context.Saga.DateTimeState = DateTime.UtcNow;
            context.Saga.Estado = false; // false porque fallo
        }).TransitionTo(RollBackEjecutado).Send(new Uri($"queue:enviar-notificacion"), context => new EnviarNotificacionMessage
        {
            CorrelationId = context.Saga.CorrelationId,
            SolicitudUsuarioId = context.Saga.SolicitudUsuarioId,
            Estado = context.Saga.Estado,
            InformacionCorreo = context.Saga.InformacionCorreo,
            SagaQueueName= context.Saga.SagaQueueName
        }).TransitionTo(NotificacionEnviada));

        During(NotificacionEnviada,
        When(NotificacionExitosaEvent)
        .Then(context =>
        {
            context.Saga.SolicitudUsuarioId = context.Message.SolicitudUsuarioId;
            context.Saga.DateTimeState = DateTime.UtcNow;
        }).TransitionTo(NotificacionExitosa).Finalize());

        SetCompletedWhenFinalized();
    }

}
