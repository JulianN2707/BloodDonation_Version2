using DonacionSangre.Domain.Dtos;
using MediatR;

namespace DonacionSangre.Application.Features.Notificaciones.Queries.NotificacionSolicitudDonacion
{
    public record struct NotificacionSolicitudDonacionQuery() : IRequest<Response<IEnumerable<Recipient>>>;
}
