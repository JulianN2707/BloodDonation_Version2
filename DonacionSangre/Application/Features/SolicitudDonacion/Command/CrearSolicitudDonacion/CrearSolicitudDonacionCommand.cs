using MediatR;

namespace DonacionSangre.Application.Features.SolicitudDonacion.Command.CrearSolicitudDonacion
{
    public class CrearSolicitudDonacionCommand : IRequest<Guid>
    {
        public required Guid CentroSaludId { get; set; }
        public required string GrupoSanguineo { get; set; }
        public required string Rh { get; set; }
    }
}
