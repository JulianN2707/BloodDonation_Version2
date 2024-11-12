using System;
using Ardalis.Specification;

namespace Archivos.Infrastructure.Repositories.ArchivosSpecification;

public interface IRepository<TEntity> : IRepositoryBase<TEntity> where TEntity : class
{
}
