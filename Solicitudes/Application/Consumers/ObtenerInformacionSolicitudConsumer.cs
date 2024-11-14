using System;
using MassTransit;
using MassTransitMessages.Messages;
using Solicitudes.Application.Specifications;
using Solicitudes.Domain.Entities;
using Solicitudes.Infrastructure.Repositories.SpecificationUnitOfWork;

namespace Solicitudes.Application.Consumers;

public class ObtenerInformacionSolicitudConsumer : IConsumer<ObtenerInformacionSolicitudMessage>
{
    private readonly ISolicitudesSpecificationUnitOfWork _solicitudesSpecificationUnitOfWork;

    public ObtenerInformacionSolicitudConsumer(ISolicitudesSpecificationUnitOfWork solicitudesSpecificationUnitOfWork)
    {
        _solicitudesSpecificationUnitOfWork = solicitudesSpecificationUnitOfWork;
    }

    public async Task Consume(ConsumeContext<ObtenerInformacionSolicitudMessage> context)
    {
        var data = context.Message;
        if (data is not null){

            var solicitud = await _solicitudesSpecificationUnitOfWork._solicitudRepository.FirstOrDefaultAsync(
                new GenericSpecification<SolicitudUsuario>(x=>x.SolicitudUsuarioId == data.SolicitudUsuarioId));
            
            CrearPersonaMessage message = new CrearPersonaMessage(){
                CorrelationId = data.CorrelationId,
                SolicitudUsuarioId = solicitud.SolicitudUsuarioId,
                Celular = solicitud.PersonaCelular,
                CorreoElectronico = solicitud.PersonaCorreoElectronico,
                Direccion = solicitud.PersonaDireccion,
                NumeroDocumento = solicitud.PersonaNumeroDocumento,
                PrimerApellido = solicitud.PersonaPrimerApellido,
                SegundoApellido = solicitud.PersonaSegundoApellido,
                PrimerNombre = solicitud.PersonaPrimerNombre,
                SegundoNombre = solicitud.PersonaSegundoNombre,
                MunicipioDireccionId = solicitud.PersonaMunicipioDireccionId,
                FechaExpedicionDocumento = (DateTime)solicitud.PersonaFechaExpedicionDocumento,
                SagaQueueName = data.SagaQueueName
            };
            var endpoint = await context.GetSendEndpoint(new Uri($"queue:crear-persona"));         
            await endpoint.Send(message);
        }
    }
}
