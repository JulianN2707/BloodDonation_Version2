using System;
using System.Reflection;
using Archivos.Domain.Common;
using Archivos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Archivos.Infrastructure.Context;

public class ArchivosContext : DbContext
{
    public ArchivosContext(DbContextOptions<ArchivosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Archivo> Archivo { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
