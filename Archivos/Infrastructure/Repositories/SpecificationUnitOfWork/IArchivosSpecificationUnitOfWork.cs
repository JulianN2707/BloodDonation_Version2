using System;

namespace Archivos.Infrastructure.Repositories.SpecificationUnitOfWork;

public interface IArchivosSpecificationUnitOfWork :IDisposable
{
    Task BeginTransaction();
    Task Rollback();
    Task<int> Save();

}
