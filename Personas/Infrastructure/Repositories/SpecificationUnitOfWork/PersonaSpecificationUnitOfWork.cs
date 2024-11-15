using System;
using Personas.Domain.Entities;
using Personas.Infrastructure.Context;
using Personas.Infrastructure.Repositories.PersonaSpecification;

namespace Personas.Infrastructure.Repositories.SpecificationUnitOfWork;

public class PersonaSpecificationUnitOfWork : IPersonaSpecificationUnitOfWork
{
    private readonly PersonasContext _personasContext;
    public IRepository<Persona> _personaRepository { get; private set; }
    public IRepository<TipoPersona> _tipoPersonaRepository { get; private set; }

    public PersonaSpecificationUnitOfWork(PersonasContext personasContext,IRepository<Persona> personaRepository,IRepository<TipoPersona> tipoPersonaRepository)
    {
        _personasContext = personasContext;
        _personaRepository = personaRepository;
        _tipoPersonaRepository = tipoPersonaRepository;
    }

    public void Dispose()
    {
        _personasContext.Dispose();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _personasContext.SaveChangesAsync();
    }
}
