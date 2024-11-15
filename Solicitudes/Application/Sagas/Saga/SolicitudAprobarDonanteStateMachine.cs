using System;
using MassTransit;
using MassTransitMessages.Messages;
using Solicitudes.Application.Sagas.StateInstances;

namespace Solicitudes.Application.Sagas.Saga;

public class SolicitudAprobarDonanteStateMachine : MassTransitStateMachine<SolicitudAprobarDonanteStateInstance>
{
    //Command
    public Event<SolicitudDonanteAprobadaMessage> SolicitudAprobadaDonanteEvent { get; set; }
    //Events
    public Event<PersonaCreadaEvent> PersonaCreadaEvent { get; set; }
    public Event<UsuarioCreadoEvent> UsuarioCreadoEvent { get; set; }
    public Event<DonanteCreadoEvent> DonanteCreadoEvent { get; set; }
    public Event<NotificacionExitosaEvent> NotificacionExitosaEvent { get; set; }

    //Error Events
    public Event<PersonaCreadaErrorEvent> PersonaCreadaErrorEvent { get; set; }
    public Event<UsuarioCreadoErrorEvent> UsuarioCreadoErrorEvent { get; set; }
    public Event<DonanteCreadoErrorEvent> DonanteCreadoErrorEvent { get; set; }

    // States
    public State SolicitudAprobadaDonante { get; private set; }
    public State PersonaCreada { get; private set; }
    public State EnviarCreacionUsuario { get; private set; }
    public State UsuarioCreado { get; private set; }
    public State RollBackEjecutado { get; private set; }
    public State DonanteCreado { get; private set; }
    public State EnviarCreacionDonante { get; private set; }
    public State NotificacionEnviada { get; private set; }
    public State NotificacionExitosa { get; private set; }
    public SolicitudAprobarDonanteStateMachine()
    {
        InstanceState(x => x.CurrentState);

        Event(() => SolicitudAprobadaDonanteEvent, x => x.CorrelateById(y => y.Message.CorrelationId));
        Event(() => PersonaCreadaEvent, x => x.CorrelateById(y => y.Message.CorrelationId));
        Event(() => UsuarioCreadoEvent, x => x.CorrelateById(y => y.Message.CorrelationId));
        Event(() => DonanteCreadoEvent, x => x.CorrelateById(y => y.Message.CorrelationId));
        Event(() => PersonaCreadaErrorEvent, x => x.CorrelateById(y => y.Message.CorrelationId));
        Event(() => UsuarioCreadoErrorEvent, x => x.CorrelateById(y => y.Message.CorrelationId));
        Event(() => DonanteCreadoErrorEvent, x => x.CorrelateById(y => y.Message.CorrelationId));
        Event(() => NotificacionExitosaEvent, x => x.CorrelateById(y => y.Message.CorrelationId));

        /*Estado Inicial es cuando la solicitud fue aprobada*/
        #region Estado Inicial
        Initially(When(SolicitudAprobadaDonanteEvent)
            .Then(context =>
            {
                context.Saga.SolicitudUsuarioId = context.Message.SolicitudUsuarioId;
                context.Saga.DateTimeState = DateTime.UtcNow;
                context.Saga.Rol = context.Message.Rol;
                context.Saga.InformacionCorreo = context.Message.InformacionCorreo;
                context.Saga.SagaQueueName = context.Message.SagaQueueName;
            }).TransitionTo(SolicitudAprobadaDonante)
                .Send(new Uri($"queue:obtener-informacion-solicitud"), context => new ObtenerInformacionSolicitudMessage
                {
                    CorrelationId = context.Saga.CorrelationId,
                    SolicitudUsuarioId = context.Saga.SolicitudUsuarioId,
                    SagaQueueName = context.Saga.SagaQueueName
                }));
        #endregion

        /*Despues se desencadena todo el flujo de la maquina de estados*/
        #region Flujo Maquina Estados
        During(SolicitudAprobadaDonante,
           When(PersonaCreadaEvent)
           .Then(context =>
           {
               context.Saga.DateTimeState = DateTime.UtcNow;
               context.Saga.SolicitudUsuarioId = context.Message.SolicitudUsuarioId;
               context.Saga.PersonaId = context.Message.PersonaId;
               context.Saga.PersonaCorreoElectronico = context.Message.CorreoElectronico;
               context.Saga.PersonaDireccion = context.Message.Direccion;
               context.Saga.PersonaNumeroCelular = context.Message.NumeroCelular;
               context.Saga.PersonaPrimerApellido = context.Message.PrimerApellido;
               context.Saga.PersonaPrimerNombre = context.Message.PrimerNombre;
               context.Saga.MunicipioDireccionId = context.Message.MunicipioDireccionId;
               context.Saga.GrupoSanguineo = context.Message.GrupoSanguineo;
               context.Saga.FactorRh = context.Message.FactorRh;
               context.Saga.Cargo = context.Message.Cargo;

           }).TransitionTo(PersonaCreada)
           .Send(new Uri($"queue:crear-usuario"), context => new CrearUsuarioMessage
           {
               CorrelationId = context.Saga.CorrelationId,
               PersonaId = context.Saga.PersonaId,
               SolicitudUsuarioId = context.Saga.SolicitudUsuarioId,
               CorreoElectronico = context.Saga.PersonaCorreoElectronico!,
               PrimerApellido = context.Saga.PersonaPrimerApellido!,
               PrimerNombre = context.Saga.PersonaPrimerNombre!,
               Rol = context.Saga.Rol,
               SagaQueueName = context.Saga.SagaQueueName
           }).TransitionTo(EnviarCreacionUsuario));


        During(EnviarCreacionUsuario,
           When(UsuarioCreadoEvent)
           .Then(context =>
           {
               context.Saga.DateTimeState = DateTime.UtcNow;
               context.Saga.SolicitudUsuarioId = context.Message.SolicitudUsuarioId;
               context.Saga.UsuarioId = context.Message.UsuarioId;
           }).TransitionTo(UsuarioCreado)
           .Send(new Uri($"queue:crear-donante"), context => new EnviarCreacionDonanteMessage
           {
               CorrelationId = context.Saga.CorrelationId,
               SolicitudUsuarioId = context.Saga.SolicitudUsuarioId,
               UsuarioId = context.Saga.UsuarioId,
               PersonaId = context.Saga.PersonaId,
               PersonaCelular = context.Saga.PersonaNumeroCelular == null ? "" : context.Saga.PersonaNumeroCelular,
               PersonaDireccion = context.Saga.PersonaDireccion == null ? "" : context.Saga.PersonaDireccion,
               CorreoElectronicoPersona = context.Saga.PersonaCorreoElectronico == null ? "" : context.Saga.PersonaCorreoElectronico,
               PersonaPrimerApellido = context.Saga.PersonaPrimerApellido == null ? "" : context.Saga.PersonaPrimerApellido,
               PersonaPrimerNombre = context.Saga.PersonaPrimerNombre == null ? "" : context.Saga.PersonaPrimerNombre,
               SagaQueueName = context.Saga.SagaQueueName,
               MunicipioDireccionId = context.Saga.MunicipioDireccionId,
               GrupoSanguineo = context.Saga.GrupoSanguineo,
               FactorRh = context.Saga.FactorRh,
               Cargo = context.Saga.Cargo

           }).TransitionTo(EnviarCreacionDonante));


        During(EnviarCreacionDonante,
            When(DonanteCreadoEvent)
            .Then(context =>
            {
                context.Saga.CorrelationId = context.Message.CorrelationId;
                context.Saga.Estado = true; // verdadero porque termino correctamente
            }).TransitionTo(DonanteCreado)
            .Send(new Uri($"queue:enviar-notificacion"), context => new EnviarNotificacionMessage
            {
                CorrelationId = context.Saga.CorrelationId,
                SolicitudUsuarioId = context.Saga.SolicitudUsuarioId,
                Estado = context.Saga.Estado,
                InformacionCorreo = context.Saga.InformacionCorreo,
                SagaQueueName = context.Saga.SagaQueueName
            }).TransitionTo(NotificacionEnviada));

       
        #endregion

        /*Si la persona natural falla en su creacion solo debo desaprobar la solicitud segun el flujo de la state machine*/
        #region Rollback 1
        DuringAny(
            When(PersonaCreadaErrorEvent)
            .Then(context =>
            {
                context.Saga.SolicitudUsuarioId = context.Message.SolicitudUsuarioId;
                context.Saga.DateTimeState = DateTime.UtcNow;
                context.Saga.Estado = false; // false porque fallo

            })
            .Send(new Uri($"queue:desaprobar-solicitud"), context => new RollBackDesaprobarSolicitudEvent
            {
                CorrelationId = context.Saga.CorrelationId,
                SolicitudUsuarioId = context.Saga.SolicitudUsuarioId
            }).TransitionTo(RollBackEjecutado)
            .Send(new Uri($"queue:enviar-notificacion"), context => new EnviarNotificacionMessage
            {
                CorrelationId = context.Saga.CorrelationId,
                SolicitudUsuarioId = context.Saga.SolicitudUsuarioId,
                Estado = context.Saga.Estado,
                InformacionCorreo = context.Saga.InformacionCorreo,
                SagaQueueName = context.Saga.SagaQueueName
            }).TransitionTo(NotificacionEnviada)
            );

        /******************************************************************************************************************/
        #endregion

        /*Si la creacion del usuario falla entonces debo desaprobar la solicitud y eliminar la persona natural*/
        #region Rollback 2
        DuringAny(
            When(UsuarioCreadoErrorEvent)
            .Then(context =>
            {
                context.Saga.SolicitudUsuarioId = context.Message.SolicitudUsuarioId;
                context.Saga.DateTimeState = DateTime.UtcNow;
                context.Saga.Estado = false; // false porque fallo

            }).Send(new Uri($"queue:desaprobar-solicitud"), context => new RollBackDesaprobarSolicitudEvent
            {
                CorrelationId = context.Saga.CorrelationId,
                SolicitudUsuarioId = context.Saga.SolicitudUsuarioId

            }).Send(new Uri($"queue:eliminar-persona"), context => new RollBackCreatePersonaEvent
            {
                CorrelationId = context.Saga.CorrelationId,
                SolicitudUsuarioId = context.Saga.SolicitudUsuarioId,
                PersonaId = context.Saga.PersonaId,
            }).TransitionTo(RollBackEjecutado)
            .Send(new Uri($"queue:enviar-notificacion"), context => new EnviarNotificacionMessage
            {
                CorrelationId = context.Saga.CorrelationId,
                SolicitudUsuarioId = context.Saga.SolicitudUsuarioId,
                Estado = context.Saga.Estado,
                InformacionCorreo = context.Saga.InformacionCorreo,
                SagaQueueName = context.Saga.SagaQueueName
            }).TransitionTo(NotificacionEnviada)
            );
        /******************************************************************************************************************/
        #endregion

        /*Si la creacion del curador falla entonces debo hacer rollback de todo lo anterior*/
        #region Rollback 3
        DuringAny(
            When(DonanteCreadoErrorEvent)
            .Then(context =>
            {
                context.Saga.SolicitudUsuarioId = context.Message.SolicitudUsuarioId;
                context.Saga.DateTimeState = DateTime.UtcNow;
                context.Saga.Estado = false; // false porque fallo

            }).Send(new Uri($"queue:desaprobar-solicitud"), context => new RollBackDesaprobarSolicitudEvent
            {
                CorrelationId = context.Saga.CorrelationId,
                SolicitudUsuarioId = context.Saga.SolicitudUsuarioId
            }).Send(new Uri($"queue:eliminar-usuario"), context => new RollBackCreateUsuarioEvent
            {
                CorrelationId = context.Saga.CorrelationId,
                SolicitudUsuarioId = context.Saga.SolicitudUsuarioId,
                UsuarioId = context.Saga.UsuarioId,
            }).Send(new Uri($"queue:eliminar-persona"), context => new RollBackCreatePersonaEvent
            {
                CorrelationId = context.Saga.CorrelationId,
                SolicitudUsuarioId = context.Saga.SolicitudUsuarioId,
                PersonaId = context.Saga.PersonaId,
            }).TransitionTo(RollBackEjecutado)
            .Send(new Uri($"queue:enviar-notificacion"), context => new EnviarNotificacionMessage
            {
                CorrelationId = context.Saga.CorrelationId,
                SolicitudUsuarioId = context.Saga.SolicitudUsuarioId,
                Estado = context.Saga.Estado,
                InformacionCorreo = context.Saga.InformacionCorreo,
                SagaQueueName = context.Saga.SagaQueueName
            }).TransitionTo(NotificacionEnviada)
            );
        /******************************************************************************************************************/
        #endregion

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
