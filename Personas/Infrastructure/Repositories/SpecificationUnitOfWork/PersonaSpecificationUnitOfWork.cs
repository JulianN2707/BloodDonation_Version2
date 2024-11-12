using System;
using Personas.Infrastructure.Context;

namespace Personas.Infrastructure.Repositories.SpecificationUnitOfWork;

public class PersonaSpecificationUnitOfWork : IPersonaSpecificationUnitOfWork
{
    private readonly PersonasContext _personasContext;

    public PersonaSpecificationUnitOfWork(PersonasContext personasContext)
    {
        _personasContext = personasContext;
    }

    public async Task BeginTransaction()
    {
        await _personasContext.Database.BeginTransactionAsync();
    }

    public async Task Rollback()
    {
        await _personasContext.Database.RollbackTransactionAsync();
    }

    public async Task<int> Save() => await _personasContext.SaveChangesAsync();

    public void Dispose()
    {
        _personasContext.Dispose();
    }
}
