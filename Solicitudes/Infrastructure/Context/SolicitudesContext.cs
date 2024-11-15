using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Solicitudes.Domain.Common;
using Solicitudes.Domain.Entities;

namespace Solicitudes.Infrastructure.Context;

public class SolicitudesContext : DbContext
{
    public SolicitudesContext(DbContextOptions<SolicitudesContext> options)
        : base(options)
    {
    }
    
    public virtual DbSet<SolicitudUsuario> SolicitudUsuario { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

}
