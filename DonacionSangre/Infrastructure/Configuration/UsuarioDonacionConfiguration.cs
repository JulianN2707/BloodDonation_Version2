using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DonacionSangre.Domain.Entities;
using DonacionSangre.Domain.ValueObjects;

namespace DonacionSangre.Infrastructure.Configuration
{
    public class UsuarioDonacionConfiguration : IEntityTypeConfiguration<UsuarioDonacion>
    {
        public void Configure(EntityTypeBuilder<UsuarioDonacion> builder)
        {
            builder.ToTable("UsuarioDonacion", "Donacion");

            builder.HasKey(sd => sd.UsuarioDonacionId);

            builder.Property(e => e.TipoSangre)
            .HasConversion(
                v => v.ToString(),  // Guardar como string usando TipoSangre.ToString()
                v => TipoSangre.CrearDesdeCadena(v)  // Convertir de string a TipoSangre
            );
        }
    }
}
