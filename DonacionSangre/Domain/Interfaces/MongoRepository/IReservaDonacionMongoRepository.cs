using DonacionSangre.Domain.ValueObjects;
using EReservaDonacion = DonacionSangre.Domain.Entities.ReservaDonacion;

namespace DonacionSangre.Domain.Interfaces.MongoRepository
{
    public interface IReservaDonacionMongoRepository
    {
        Task UpdateReservasDonacionAsync(List<EReservaDonacion> reservasDonacion);
    }
}
