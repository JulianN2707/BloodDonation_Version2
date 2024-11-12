using DonacionSangre.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DonacionSangre.Infrastructure.Configuration
{
    public class DepartamentoConfiguration : IEntityTypeConfiguration<Departamento>
    {
        public void Configure(EntityTypeBuilder<Departamento> builder)
        {
            builder.ToTable("Departamento", "Donacion");

            builder.HasKey(d => d.DepartamentoId);  // Define la llave primaria

            builder.Property(d => d.Nombre)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
