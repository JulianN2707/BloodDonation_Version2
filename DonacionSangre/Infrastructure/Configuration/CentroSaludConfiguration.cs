using DonacionSangre.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DonacionSangre.Infrastructure.Configuration
{
    public class CentroSaludConfiguration : IEntityTypeConfiguration<CentroSalud>
    {
        public void Configure(EntityTypeBuilder<CentroSalud> builder)
        {
            builder.ToTable("CentroSalud", "Donacion");

            builder.HasKey(cs => cs.CentroSaludId);  // Define la llave primaria

            builder.Property(cs => cs.Nombre)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasOne(cs => cs.Municipio)
                .WithMany(m => m.CentrosSalud)
                .HasForeignKey(cs => cs.MunicipioId);  // Configura la relación con Municipio
        }
    }
}
