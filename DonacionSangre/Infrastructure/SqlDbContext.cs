using DonacionSangre.Domain.Entities;
using DonacionSangre.Domain.Entities.Test;
using DonacionSangre.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DonacionSangre.Infrastructure
{
    public class SqlDbContext : DbContext
    {
        public DbSet<Donante> Donantes { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<TipoPersona> TipoPersonas { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Municipio> Municipios { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<CentroSalud> CentrosSalud { get; set; }
        public DbSet<SolicitudDonacion> SolicitudesDonacion { get; set; }
        public DbSet<ReservaDonacion> ReservasDonacion { get; set; }

        // Constructor que recibe las opciones de configuración
        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options) { }

        // Configuración del modelo, si necesitas especificar detalles adicionales
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
