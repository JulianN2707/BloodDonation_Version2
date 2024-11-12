using Ardalis.Specification.EntityFrameworkCore;
using Solicitudes.Infrastructure.Context;

namespace Solicitudes.Infrastructure.Repositories.SolicitudesSpecification;

public class Repository<TEntity>(SolicitudesContext solicitudesContext) : RepositoryBase<TEntity>(solicitudesContext), IRepository<TEntity> where TEntity : class
{
    public override async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await solicitudesContext.Set<TEntity>().AddAsync(entity, cancellationToken);
        return entity;
    }
    public override async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entity, CancellationToken cancellationToken = default)
    {
        await solicitudesContext.Set<TEntity>().AddRangeAsync(entity, cancellationToken);
        return entity;
    }

    public override Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        solicitudesContext.Set<TEntity>().Update(entity);
        return Task.FromResult(entity);
    }

    public override Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        solicitudesContext.Set<TEntity>().Remove(entity);
        return Task.CompletedTask;
    }
}

