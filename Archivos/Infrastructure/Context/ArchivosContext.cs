using System;
using System.Reflection;
using Archivos.Domain.Common;
using Archivos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Archivos.Infrastructure.Context;

public class ArchivosContext :DbContext
{
    private readonly AppSettings _appSettingsOptions;

    public ArchivosContext()
    {
    }

    public ArchivosContext(DbContextOptions<ArchivosContext> options, IOptionsSnapshot<AppSettings> appSettingsOptions)
        : base(options)
    {
        _appSettingsOptions = appSettingsOptions.Value;
    }
    
    public virtual DbSet<Archivo> Archivo { get; set; }
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
