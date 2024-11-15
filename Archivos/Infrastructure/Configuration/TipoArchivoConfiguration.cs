using Archivos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Archivos.Infrastructure.Configuration
{
    public class TipoArchivoConfiguration : IEntityTypeConfiguration<TipoArchivo>
    {
        public void Configure(EntityTypeBuilder<TipoArchivo> builder)
        {
            builder.ToTable("TipoArchivo", "Archivo");
            builder.HasKey(x => x.TipoArchivoId);
        }
    }
}
