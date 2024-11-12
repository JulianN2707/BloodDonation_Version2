using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Solicitudes.Domain.Common;
using Solicitudes.Domain.Entities;

namespace Solicitudes.Infrastructure.Context;

public class SolicitudesContext : DbContext
{
     private readonly AppSettings _appSettingsOptions;

    public SolicitudesContext()
    {
    }

    public SolicitudesContext(DbContextOptions<SolicitudesContext> options, IOptionsSnapshot<AppSettings> appSettingsOptions)
        : base(options)
    {
        _appSettingsOptions = appSettingsOptions.Value;
    }
    
    public virtual DbSet<SolicitudUsuario> SolicitudUsuario { get; set; }
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
