using Microsoft.EntityFrameworkCore;
using EDonante = DonacionSangre.Domain.Entities.Test.Donante;

namespace DonacionSangre.Infrastructure.SqlServerRepositories.Donante
{
    public class DonanteRepository : IDonanteRepository
    {
        private readonly SqlDbContext _context;

        public DonanteRepository(SqlDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(EDonante donante)
        {
            await _context.Donantes.AddAsync(donante);
            await _context.SaveChangesAsync();
        }

        public async Task<EDonante> GetByIdAsync(Guid id)
        {
            return await _context.Donantes.FindAsync(id);
        }

        public async Task<List<EDonante>> GetAllAsync()
        {
            return await _context.Donantes.ToListAsync();
        }

        public async Task UpdateAsync(EDonante donante)
        {
            _context.Donantes.Update(donante);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var donante = await GetByIdAsync(id);
            if (donante != null)
            {
                _context.Donantes.Remove(donante);
                await _context.SaveChangesAsync();
            }
        }
    }
}
