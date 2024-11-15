using MediatR;
using Solicitudes.Domain.Dto;

namespace Solicitudes.Application.Features.SolicitudDonante.Queries.ObtenerSolicitudesDonante
{
    public class ObtenerSolicitudesDonanteQuery : IRequest<List<ObtenerSolicitudesDonanteResponse>>
    {
    }
}
