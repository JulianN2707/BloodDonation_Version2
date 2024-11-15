using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Personas.Domain.Entities;
using Personas.Domain.ValueObjects;

namespace Personas.Infrastructure.Configuration
{
    public class PersonaConfiguration : IEntityTypeConfiguration<Persona>
    {
        public void Configure(EntityTypeBuilder<Persona> builder)
        {
            builder.ToTable("Persona", "Persona");
            builder.HasKey(x => x.PersonaId);

            builder.HasOne(x => x.TipoPersona).WithMany(x => x.Personas)
                .HasForeignKey(x => x.TipoPersonaId);

            builder.Property(e => e.TipoSangre)
            .HasConversion(
                v => v.ToString(),  // Guardar como string usando TipoSangre.ToString()
                v => TipoSangre.CrearDesdeCadena(v)  // Convertir de string a TipoSangre
            );
        }
    }
}
