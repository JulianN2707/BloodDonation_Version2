using Archivos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Archivos.Infrastructure.Configuration
{
    public class ArchivoConfiguration : IEntityTypeConfiguration<Archivo>
    {
        public void Configure(EntityTypeBuilder<Archivo> builder)
        {
            builder.ToTable("Archivo", "Archivo");
            builder.HasKey(x => x.ArchivoId);

            builder.HasOne(x => x.TipoArchivo).WithMany(x => x.Archivos)
                .HasForeignKey(x => x.TipoArchivoId);
        }
    }
}
