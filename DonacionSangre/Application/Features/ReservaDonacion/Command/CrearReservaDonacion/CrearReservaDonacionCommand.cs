using MediatR;

namespace DonacionSangre.Application.Features.ReservaDonacion.Command.CrearReservaDonacion
{
    public class CrearReservaDonacionCommand : IRequest<Guid>
    {
        public Guid PersonaId { get; set; }
        public DateTime FechaDonacion {  get; set; }
    }
}
