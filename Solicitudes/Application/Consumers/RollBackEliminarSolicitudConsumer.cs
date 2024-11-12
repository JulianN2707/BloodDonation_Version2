using System;
using MassTransit;
using MassTransitMessages.Messages;
using Solicitudes.Application.Specifications;
using Solicitudes.Domain.Entities;
using Solicitudes.Infrastructure.Repositories.SpecificationUnitOfWork;

namespace Solicitudes.Application.Consumers;

public class RollBackEliminarSolicitudConsumer : IConsumer<RollBackCrearSolicitudEvent>
{
    private readonly ISolicitudesSpecificationUnitOfWork _solicitudesSpecificationUnitOfWork;
    private readonly ILogger<RollBackEliminarSolicitudConsumer> _logger;

    public RollBackEliminarSolicitudConsumer(ISolicitudesSpecificationUnitOfWork solicitudesSpecificationUnitOfWork
        , ILogger<RollBackEliminarSolicitudConsumer> logger)
    {
        _solicitudesSpecificationUnitOfWork = solicitudesSpecificationUnitOfWork;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<RollBackCrearSolicitudEvent> context)
    {
        var data = context.Message;
        if (data is not null)
        {
            var solicitud = await _solicitudesSpecificationUnitOfWork._solicitudRepository.FirstOrDefaultAsync(
                new GenericSpecification<SolicitudUsuario>(x=>x.SolicitudUsuarioId == data.SolicitudUsuarioId));
            if (solicitud is not null)
            {
               await _solicitudesSpecificationUnitOfWork._solicitudRepository.DeleteAsync(solicitud);
                await _solicitudesSpecificationUnitOfWork._solicitudRepository.SaveChangesAsync();

                var rollBackEjecutadoEvent = new RollBackCrearSolicitudEjecutadoEvent
                {
                    CorrelationId = data.CorrelationId,
                    SolicitudUsuarioId = data.SolicitudUsuarioId,
                };
                var endpoint = await context.GetSendEndpoint(new Uri($"queue:{data.SagaQueueName}"));
                await endpoint.Send(rollBackEjecutadoEvent);

                _logger.LogInformation($"BUS:rollBackCrearSolicitudEjecutadoEvent event consumed. Message: {context.Message.CorrelationId}");
            }
            else
            {
                _logger.LogInformation($"BUS:rollBackCrearSolicitudEjecutadoEvent event Error. Message: solicitud id doesnt exist");
            }
        }
    }
}
