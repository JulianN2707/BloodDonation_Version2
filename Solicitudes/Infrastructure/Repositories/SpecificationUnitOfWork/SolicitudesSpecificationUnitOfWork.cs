using System;
using Solicitudes.Infrastructure.Context;

namespace Solicitudes.Infrastructure.Repositories.SpecificationUnitOfWork;

public class SolicitudesSpecificationUnitOfWork : ISolicitudesSpecificationUnitOfWork
{
    private readonly SolicitudesContext _solicitudesContext;

    public SolicitudesSpecificationUnitOfWork(SolicitudesContext solicitudesContext)
    {
        _solicitudesContext = solicitudesContext;
    }

    public async Task BeginTransaction()
        {
            await _solicitudesContext.Database.BeginTransactionAsync();
        }

        public async Task Rollback()
        {
            await _solicitudesContext.Database.RollbackTransactionAsync();
        }

        public async Task<int> Save() => await _solicitudesContext.SaveChangesAsync();

        public void Dispose()
        {
            _solicitudesContext.Dispose();
        }

}
