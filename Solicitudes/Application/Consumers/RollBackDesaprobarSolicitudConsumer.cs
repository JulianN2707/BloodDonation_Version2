using MassTransit;
using MassTransitMessages.Messages;
using Solicitudes.Application.Specifications;
using Solicitudes.Domain.Entities;
using Solicitudes.Infrastructure.Repositories.SpecificationUnitOfWork;

namespace Solicitudes.Application.Consumers;

public class RollBackDesaprobarSolicitudConsumer : IConsumer<RollBackDesaprobarSolicitudEvent>
{
    private readonly ISolicitudesSpecificationUnitOfWork _solicitudesSpecificationUnitOfWork;
    private readonly ILogger<RollBackEliminarSolicitudConsumer> _logger;

    public RollBackDesaprobarSolicitudConsumer(ISolicitudesSpecificationUnitOfWork solicitudesSpecificationUnitOfWork,
        ILogger<RollBackEliminarSolicitudConsumer> logger)
    {
        _solicitudesSpecificationUnitOfWork = solicitudesSpecificationUnitOfWork;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<RollBackDesaprobarSolicitudEvent> context)
    {
        var data = context.Message;
        if (data is not null)
        {
            var solicitud = await _solicitudesSpecificationUnitOfWork._solicitudRepository.FirstOrDefaultAsync(
                new GenericSpecification<SolicitudUsuario>(x=>x.SolicitudUsuarioId == data.SolicitudUsuarioId));
            

            if (solicitud is not null)
            {
                var estadoSolicitudUsuarioRechazada = new Guid("B05A7F4A-4D21-4C4D-BF76-19C51F1F25C7");
                solicitud.FechaRechazo = DateTime.UtcNow;
                solicitud.EstadoSolicitudUsuarioId = estadoSolicitudUsuarioRechazada;
                await _solicitudesSpecificationUnitOfWork._solicitudRepository.UpdateAsync(solicitud);
                await _solicitudesSpecificationUnitOfWork.SaveChangesAsync();
                _logger.LogInformation($"IRollBackDesaprobarSolicitudEvent event Consumed.");
            }
            else
            {
                _logger.LogInformation($"IRollBackDesaprobarSolicitudEvent event Error.");
            }
        }
    }
}

