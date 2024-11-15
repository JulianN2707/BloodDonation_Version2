using DonacionSangre.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DonacionSangre.Infrastructure
{
    public class SqlDbContext : DbContext
    {
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
