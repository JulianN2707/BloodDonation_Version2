using System;
using Personas.Domain.Entities;
using Personas.Infrastructure.Repositories.PersonaSpecification;

namespace Personas.Infrastructure.Repositories.SpecificationUnitOfWork;

public interface IPersonaSpecificationUnitOfWork : IDisposable
{

   public IRepository<Persona> _personaRepository { get; }
   public IRepository<TipoPersona> _tipoPersonaRepository { get; }

    Task<int> SaveChangesAsync();

}
