using System;

namespace Usuarios.Infrastructure.Repositories.SpecificationUnitOfWork;

public interface IUsuariosSpecificationUnitOfWork : IDisposable
{
    Task BeginTransaction();
    Task Rollback();
    Task<int> Save();

}
