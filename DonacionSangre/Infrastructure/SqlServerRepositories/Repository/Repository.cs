
using Ardalis.Specification.EntityFrameworkCore;
using DonacionSangre.Domain.Interfaces.SqlServerRepository;

namespace DonacionSangre.Infrastructure.SqlServerRepositories.Repository
{
    public class Repository<T>(SqlDbContext dbContext) : RepositoryBase<T>(dbContext), IRepository<T> where T : class
    {
    }
}
