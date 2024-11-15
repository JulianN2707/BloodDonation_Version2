using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Personas.Domain.Common;
using Personas.Domain.Entities;

namespace Personas.Infrastructure.Context;

public class PersonasContext : DbContext
{
    public PersonasContext(DbContextOptions<PersonasContext> options)
        : base(options)
    {
    }
    
    public virtual DbSet<Persona> Persona { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
