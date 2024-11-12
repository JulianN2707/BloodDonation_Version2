using System;

namespace Personas.Infrastructure.Repositories.SpecificationUnitOfWork;

public interface IPersonaSpecificationUnitOfWork : IDisposable
{

    Task BeginTransaction();
    Task Rollback();
    Task<int> Save();

}
