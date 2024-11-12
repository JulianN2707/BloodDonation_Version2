using Personas.Infrastructure.Context;
using Ardalis.Specification.EntityFrameworkCore;



namespace Personas.Infrastructure.Repositories.PersonaSpecification;


public class Repository<TEntity>(PersonasContext personasContext) : RepositoryBase<TEntity>(personasContext), IRepository<TEntity> where TEntity : class
{
    public override async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await personasContext.Set<TEntity>().AddAsync(entity, cancellationToken);
        return entity;
    }
    public override async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entity, CancellationToken cancellationToken = default)
    {
        await personasContext.Set<TEntity>().AddRangeAsync(entity, cancellationToken);
        return entity;
    }

    public override Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        personasContext.Set<TEntity>().Update(entity);
        return Task.FromResult(entity);
    }

    public override Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        personasContext.Set<TEntity>().Remove(entity);
        return Task.CompletedTask;
    }
}
