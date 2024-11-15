using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solicitudes.Domain.Entities;

namespace Solicitudes.Infrastructure.Configuration
{
    public class EstadoSolicitudUsuarioConfiguration : IEntityTypeConfiguration<EstadoSolicitudUsuario>
    {
        public void Configure(EntityTypeBuilder<EstadoSolicitudUsuario> builder)
        {
            builder.ToTable("EstadoSolicitudUsuario", "SolicitudUsuario");
            builder.HasKey(x => x.EstadoSolicitudUsuarioId);
        }
    }
}
