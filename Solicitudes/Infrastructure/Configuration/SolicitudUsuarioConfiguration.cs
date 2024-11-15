using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solicitudes.Domain.Entities;
using Solicitudes.Domain.ValueObjects;

namespace Solicitudes.Infrastructure.Configuration
{
    public class SolicitudUsuarioConfiguration : IEntityTypeConfiguration<SolicitudUsuario>
    {
        public void Configure(EntityTypeBuilder<SolicitudUsuario> builder)
        {
            builder.ToTable("SolicitudUsuario", "SolicitudUsuario");
            builder.HasKey(x => x.SolicitudUsuarioId);
            builder.HasOne(x => x.EstadoSolicitudUsuario).WithMany(x => x.SolicitudesUsuario)
                .HasForeignKey(x => x.EstadoSolicitudUsuarioId);

            builder.Property(e => e.TipoSangre)
            .HasConversion(
                v => v.ToString(),  // Guardar como string usando TipoSangre.ToString()
                v => TipoSangre.CrearDesdeCadena(v)  // Convertir de string a TipoSangre
            );
        }
    }
}
