using Ardalis.Specification;

namespace Personas.Infrastructure.Repositories.PersonaSpecification;

public interface IRepository<TEntity> : IRepositoryBase<TEntity> where TEntity : class
{
}
