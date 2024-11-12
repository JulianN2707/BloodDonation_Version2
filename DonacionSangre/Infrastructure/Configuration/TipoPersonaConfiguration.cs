using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DonacionSangre.Domain.Entities;

namespace DonacionSangre.Infrastructure.Configuration
{
    public class TipoPersonaConfiguration : IEntityTypeConfiguration<TipoPersona>
    {
        public void Configure(EntityTypeBuilder<TipoPersona> builder)
        {
            builder.ToTable("TipoPersona", "Donacion");
            builder.HasKey(tp => tp.TipoPersonaId);  // Define la llave primaria

            builder.Property(tp => tp.Descripcion)
                .IsRequired()
                .HasMaxLength(100);  // Configura el campo Descripcion

            builder.HasMany(tp => tp.Personas)
                .WithOne(p => p.TipoPersona)
                .HasForeignKey(p => p.TipoPersonaId);
        }
    }
}
