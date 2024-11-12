using DonacionSangre.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DonacionSangre.Infrastructure.Configuration
{
    public class MunicipioConfiguration : IEntityTypeConfiguration<Municipio>
    {
        public void Configure(EntityTypeBuilder<Municipio> builder)
        {
            builder.ToTable("Municipio", "Donacion");

            builder.HasKey(m => m.MunicipioId);  // Define la llave primaria

            builder.Property(m => m.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(m => m.Departamento)
                .WithMany(d => d.Municipios)
                .HasForeignKey(m => m.DepartamentoId);  // Configura la relación con Departamento
        }
    }
}
