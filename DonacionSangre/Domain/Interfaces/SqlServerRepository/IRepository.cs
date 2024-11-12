using Ardalis.Specification;

namespace DonacionSangre.Domain.Interfaces.SqlServerRepository
{
    public interface IRepository<T> : IRepositoryBase<T> where T : class
    {
    }
}
