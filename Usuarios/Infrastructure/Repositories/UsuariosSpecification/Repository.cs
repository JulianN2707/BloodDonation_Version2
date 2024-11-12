using System;
using Ardalis.Specification.EntityFrameworkCore;
using Usuarios.Infrastructure.Context;

namespace Usuarios.Infrastructure.Repositories.UsuariosSpecification;

public class Repository<TEntity>(UsuariosContext usuariosContext) : RepositoryBase<TEntity>(usuariosContext), IRepository<TEntity> where TEntity : class
{
    public override async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await usuariosContext.Set<TEntity>().AddAsync(entity, cancellationToken);
        return entity;
    }
    public override async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entity, CancellationToken cancellationToken = default)
    {
        await usuariosContext.Set<TEntity>().AddRangeAsync(entity, cancellationToken);
        return entity;
    }

    public override Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        usuariosContext.Set<TEntity>().Update(entity);
        return Task.FromResult(entity);
    }

    public override Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        usuariosContext.Set<TEntity>().Remove(entity);
        return Task.CompletedTask;
    }
}
