using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Usuarios.Domain.Common;
using Usuarios.Domain.Entities;

namespace Usuarios.Infrastructure.Context;

public class UsuariosContext :DbContext
{

    public UsuariosContext(DbContextOptions<UsuariosContext> options)
        : base(options)
    {
    }
    
    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

}
