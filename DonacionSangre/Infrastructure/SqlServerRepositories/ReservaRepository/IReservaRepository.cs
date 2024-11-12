using EReserva = DonacionSangre.Domain.Entities.Test.Reserva;
namespace DonacionSangre.Infrastructure.SqlServerRepositories.Reserva
{
    public interface IReservaRepository
    {
        Task AddAsync(EReserva reserva);
        Task<EReserva> GetByIdAsync(Guid id);
        Task<List<EReserva>> GetAllAsync();
        Task UpdateAsync(EReserva reserva);
        Task DeleteAsync(Guid id);
    }
}
