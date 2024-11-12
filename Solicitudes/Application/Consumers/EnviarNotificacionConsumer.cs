using System;
using MassTransit;
using MassTransitMessages.Messages;
using Solicitudes.Infrastructure.Services.NotificacionesService;

namespace Solicitudes.Application.Consumers;

public class EnviarNotificacionConsumer : IConsumer<EnviarNotificacionMessage>
{
    private readonly IEmailService _emailService;
    private readonly ILogger<EnviarNotificacionConsumer> _logger;

    public EnviarNotificacionConsumer(IEmailService emailService,
        ILogger<EnviarNotificacionConsumer> logger)
    {
        _emailService = emailService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<EnviarNotificacionMessage> context)
    {
        var data = context.Message;
        if (data is not null)
        {
            if (data.Estado == true)
            {
                foreach (var item in data.InformacionCorreo)
                {
                    string bodyContent = _emailService.GetEMailBody(item.nombreTemplate, item.informacionCorreo);
                    await _emailService.SendNotification(bodyContent, item.recipientes);
                }
            }
            else //Si el estado fue false se debe enviar un correo indicando que fallo
            {
                foreach (var item in data.InformacionCorreo)
                {
                    string bodyContent = _emailService.GetEMailBody(item.nombreTemplateError, item.informacionCorreo);
                    await _emailService.SendNotification(bodyContent, item.recipientes);
                }
            }
            var notificacion = new NotificacionExitosaEvent
            {
                CorrelationId = data.CorrelationId,
                SolicitudUsuarioId = data.SolicitudUsuarioId,
            };
            
            var endpoint = await context.GetSendEndpoint(new Uri($"queue:{data.SagaQueueName}"));
            await endpoint.Send(notificacion);
            _logger.LogInformation($"BUS:NotificacionExitosaEvent event publish. Message: {context.Message.CorrelationId}");
        }

    }

}
