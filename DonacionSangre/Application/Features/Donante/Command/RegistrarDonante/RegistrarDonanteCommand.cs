using MediatR;

namespace DonacionSangre.Application.Features.Donante.Command.RegistrarDonante
{
    public class RegistrarDonanteCommand : IRequest<Guid>
    {
        public required string  Nombre { get; set; } 
        public required string TipoSangre { get; set; }
        public required string ZonaResidencia { get; set; }
    }
}
