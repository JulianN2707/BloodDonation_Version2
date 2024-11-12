using EDonante = DonacionSangre.Domain.Entities.Test.Donante;
namespace DonacionSangre.Infrastructure.SqlServerRepositories.Donante
{
    public interface IDonanteRepository
    {
        Task AddAsync(EDonante donante);
        Task<EDonante> GetByIdAsync(Guid id);
        Task<List<EDonante>> GetAllAsync();
        Task UpdateAsync(EDonante donante);
        Task DeleteAsync(Guid id);
    }
}
