using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Personas.Domain.Common;
using Personas.Domain.Entities;

namespace Personas.Infrastructure.Context;

public class PersonasContext : DbContext
{
    private readonly AppSettings _appSettingsOptions;

    public PersonasContext()
    {
    }

    public PersonasContext(DbContextOptions<PersonasContext> options, IOptionsSnapshot<AppSettings> appSettingsOptions)
        : base(options)
    {
        _appSettingsOptions = appSettingsOptions.Value;
    }
    
    public virtual DbSet<Persona> Persona { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer(_appSettingsOptions.ConnectionString, options => options.EnableRetryOnFailure
                (maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null));

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

}
