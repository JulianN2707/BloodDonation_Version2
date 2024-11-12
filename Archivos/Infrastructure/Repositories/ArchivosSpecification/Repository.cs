using System;
using Archivos.Infrastructure.Context;
using Ardalis.Specification.EntityFrameworkCore;

namespace Archivos.Infrastructure.Repositories.ArchivosSpecification;

public class Repository<TEntity>(ArchivosContext archivosContext) : RepositoryBase<TEntity>(archivosContext), IRepository<TEntity> where TEntity : class
{
    public override async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await archivosContext.Set<TEntity>().AddAsync(entity, cancellationToken);
        return entity;
    }
    public override async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entity, CancellationToken cancellationToken = default)
    {
        await archivosContext.Set<TEntity>().AddRangeAsync(entity, cancellationToken);
        return entity;
    }

    public override Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        archivosContext.Set<TEntity>().Update(entity);
        return Task.FromResult(entity);
    }

    public override Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        archivosContext.Set<TEntity>().Remove(entity);
        return Task.CompletedTask;
    }
}
