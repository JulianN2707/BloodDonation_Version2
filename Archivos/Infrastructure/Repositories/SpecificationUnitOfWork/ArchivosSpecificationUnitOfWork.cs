using System;
using Archivos.Infrastructure.Context;

namespace Archivos.Infrastructure.Repositories.SpecificationUnitOfWork;

public class ArchivosSpecificationUnitOfWork : IArchivosSpecificationUnitOfWork
{

    private readonly ArchivosContext _archivosContext;

    public ArchivosSpecificationUnitOfWork(ArchivosContext archivosContext)
    {
        _archivosContext = archivosContext;
    }

    public async Task BeginTransaction()
    {
        await _archivosContext.Database.BeginTransactionAsync();
    }

    public async Task Rollback()
    {
        await _archivosContext.Database.RollbackTransactionAsync();
    }

    public async Task<int> Save() => await _archivosContext.SaveChangesAsync();

    public void Dispose()
    {
        _archivosContext.Dispose();
    }

}
