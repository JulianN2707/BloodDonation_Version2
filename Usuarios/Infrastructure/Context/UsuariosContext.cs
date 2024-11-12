using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Usuarios.Domain.Common;
using Usuarios.Domain.Entities;

namespace Usuarios.Infrastructure.Context;

public class UsuariosContext :DbContext
{
     private readonly AppSettings _appSettingsOptions;

    public UsuariosContext()
    {
    }

    public UsuariosContext(DbContextOptions<UsuariosContext> options, IOptionsSnapshot<AppSettings> appSettingsOptions)
        : base(options)
    {
        _appSettingsOptions = appSettingsOptions.Value;
    }
    
    public virtual DbSet<Usuario> Usuario { get; set; }
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
