using System;
using Ardalis.Specification;

namespace Solicitudes.Infrastructure.Repositories.SolicitudesSpecification;

public interface IRepository<TEntity> : IRepositoryBase<TEntity> where TEntity : class
{
}
