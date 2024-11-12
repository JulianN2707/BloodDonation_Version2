using System;

namespace Solicitudes.Infrastructure.Repositories.SpecificationUnitOfWork;

public interface ISolicitudesSpecificationUnitOfWork : IDisposable
{
    Task BeginTransaction();
    Task Rollback();
    Task<int> Save();

}
