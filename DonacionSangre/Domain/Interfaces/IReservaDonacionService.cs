using DonacionSangre.Domain.Entities;

namespace DonacionSangre.Domain.Interfaces
{
    public interface IReservaDonacionService
    {
        Task<ReservaDonacion> CrearReservaAsync(Persona persona, DateTime fechaReserva);
    }
}
