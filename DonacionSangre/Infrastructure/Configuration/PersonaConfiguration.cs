using DonacionSangre.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DonacionSangre.Domain.ValueObjects;

namespace DonacionSangre.Infrastructure.Configuration
{
    public class PersonaConfiguration : IEntityTypeConfiguration<Persona>
    {
        public void Configure(EntityTypeBuilder<Persona> builder)
        {
            builder.ToTable("Persona", "Donacion");

            builder.HasKey(p => p.PersonaId);

            builder.Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Apellido)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(p => p.TipoPersona)
                .WithMany(tp => tp.Personas)
                .HasForeignKey(p => p.TipoPersonaId);  // Configura la relación con TipoPersona

            builder.HasOne(p => p.Municipio)
                .WithMany(m => m.Personas)
                .HasForeignKey(p => p.MunicipioId);  // Configura la relación con Municipio

            builder.Property(e => e.TipoSangre)
                .HasConversion(
                    v => v.ToString(),  // Guardar como string usando TipoSangre.ToString()
                    v => TipoSangre.CrearDesdeCadena(v)  // Convertir de string a TipoSangre
                );

            builder.HasOne(p => p.CentroSalud)
                .WithMany(m => m.Personas)
                .HasForeignKey(p => p.CentroSaludId)
                .IsRequired(false);  // Relación opcional
        }
    }
}
