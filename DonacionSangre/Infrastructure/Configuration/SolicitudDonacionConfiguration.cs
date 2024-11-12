using DonacionSangre.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DonacionSangre.Domain.ValueObjects;

namespace DonacionSangre.Infrastructure.Configuration
{
    public class SolicitudDonacionConfiguration : IEntityTypeConfiguration<SolicitudDonacion>
    {
        public void Configure(EntityTypeBuilder<SolicitudDonacion> builder)
        {
            builder.ToTable("SolicitudDonacion", "Donacion");

            builder.HasKey(sd => sd.SolicitudDonacionId);  // Define la llave primaria

            builder.Property(sd => sd.FechaSolicitud)
                .IsRequired();

            builder.Property(e => e.TipoSangre)
            .HasConversion(
                v => v.ToString(),  // Guardar como string usando TipoSangre.ToString()
                v => TipoSangre.CrearDesdeCadena(v)  // Convertir de string a TipoSangre
            );
            builder.Property(e => e.Estado)
            .HasConversion(
                v => v.ToString(),  // Guardar como string usando EstadoSolicitudDonacion.ToString()
                v => EstadoSolicitudDonacion.CrearDesdeCadena(v)  // Convertir de string a EstadoSolicitudDonacion
            );

            builder.HasOne(sd => sd.CentroSalud)
                .WithMany(cs => cs.SolicitudesDonacion)
                .HasForeignKey(sd => sd.CentroSaludId);  // Configura la relación con CentroSalud
        }
    }
}
