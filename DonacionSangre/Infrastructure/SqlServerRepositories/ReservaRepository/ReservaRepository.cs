using Microsoft.EntityFrameworkCore;
using EReserva = DonacionSangre.Domain.Entities.Test.Reserva;

namespace DonacionSangre.Infrastructure.SqlServerRepositories.Reserva
{
    public class ReservaRepository : IReservaRepository
    {
        private readonly SqlDbContext _context;

        public ReservaRepository(SqlDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(EReserva reserva)
        {
            await _context.Reservas.AddAsync(reserva);
            await _context.SaveChangesAsync();
        }

        public async Task<EReserva> GetByIdAsync(Guid id)
        {
            return await _context.Reservas.FindAsync(id);
        }

        public async Task<List<EReserva>> GetAllAsync()
        {
            return await _context.Reservas.ToListAsync();
        }

        public async Task UpdateAsync(EReserva reserva)
        {
            _context.Reservas.Update(reserva);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var reserva = await GetByIdAsync(id);
            if (reserva != null)
            {
                _context.Reservas.Remove(reserva);
                await _context.SaveChangesAsync();
            }
        }
    }
}
