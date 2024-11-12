using System;
using Ardalis.Specification;

namespace Usuarios.Infrastructure.Repositories.UsuariosSpecification;

public interface IRepository<TEntity> : IRepositoryBase<TEntity> where TEntity : class
{
}
