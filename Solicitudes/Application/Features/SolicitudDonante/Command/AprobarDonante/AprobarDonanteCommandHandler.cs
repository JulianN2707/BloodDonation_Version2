using System;
using System.Collections;
using MassTransit;
using MassTransitMessages.Messages;
using MediatR;
using Solicitudes.Application.Specifications;
using Solicitudes.Domain.Entities;
using Solicitudes.Infrastructure.Repositories.SpecificationUnitOfWork;

namespace Solicitudes.Application.Features.SolicitudDonante.Command.AprobarDonante;

public class AprobarDonanteCommandHandler : IRequestHandler<AprobarDonanteCommand, bool>
{
    private readonly ISolicitudesSpecificationUnitOfWork _solicitudesSpecificationUnitOfWork;
    private readonly IBus _massTransitService;

    public AprobarDonanteCommandHandler(ISolicitudesSpecificationUnitOfWork solicitudesSpecificationUnitOfWork, IBus massTransitService)
    {
        _solicitudesSpecificationUnitOfWork = solicitudesSpecificationUnitOfWork;
        _massTransitService = massTransitService;
    }

    public async Task<bool> Handle(AprobarDonanteCommand request, CancellationToken cancellationToken)
    {
        var solicitud = await _solicitudesSpecificationUnitOfWork._solicitudRepository.FirstOrDefaultAsync(
            new GenericSpecification<SolicitudUsuario>(x=>x.SolicitudUsuarioId == request.SolicitudUsuarioId)
        );
        solicitud.EstadoSolicitudUsuarioId=1;
        await _solicitudesSpecificationUnitOfWork._solicitudRepository.UpdateAsync(solicitud);
        await _solicitudesSpecificationUnitOfWork.SaveChangesAsync();

        var informacionCorreos = await CrearCorreosAEnviar(solicitud);

        SolicitudDonanteAprobadaMessage message = new SolicitudDonanteAprobadaMessage(){
            CorrelationId = NewId.NextSequentialGuid(),
            SolicitudUsuarioId = request.SolicitudUsuarioId,
            Rol = "donante",
            InformacionCorreo = informacionCorreos,
            SagaQueueName = $"saga-aprobar-donante"
        };
        var endpoint = await _massTransitService.GetSendEndpoint(new Uri($"queue:-saga-aprobar-donante"));
        await endpoint.Send(message);
        return true;
    }

     public async Task<List<EnviarCorreoDto>> CrearCorreosAEnviar(SolicitudUsuario detalle)
    {
        var listaCorreoDto = new List<EnviarCorreoDto>();
        Hashtable data = new()
            {
                { "USUARIO", detalle.PersonaCorreoElectronico == null ? "" : detalle.PersonaCorreoElectronico}
            };
        var correoAdmin = new EnviarCorreoDto
        {
            nombreTemplate = "aprobacionSolicitud.html",
            nombreTemplateError = "aprobacionSolicitudError.html",
            recipientes = new List<string> { detalle.PersonaCorreoElectronico == null ? "" : detalle.PersonaCorreoElectronico },
            informacionCorreo = data
        };
        listaCorreoDto.Add(correoAdmin);

        return listaCorreoDto;
    }
}
