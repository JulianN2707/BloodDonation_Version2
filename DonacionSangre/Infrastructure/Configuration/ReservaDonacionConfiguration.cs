using DonacionSangre.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DonacionSangre.Domain.ValueObjects;

namespace DonacionSangre.Infrastructure.Configuration
{
    public class ReservaDonacionConfiguration : IEntityTypeConfiguration<ReservaDonacion>
    {
        public void Configure(EntityTypeBuilder<ReservaDonacion> builder)
        {
            builder.ToTable("ReservaDonacion", "Donacion");

            builder.HasKey(rd => rd.ReservaDonacionId);

            builder.Property(rd => rd.FechaReserva)
                .IsRequired();

            builder.HasOne(rd => rd.Persona)
                .WithMany(p => p.ReservasDonacion)
                .HasForeignKey(rd => rd.PersonaId);

            builder.HasOne(m => m.SolicitudDonacion)
                .WithMany(d => d.ReservasDonacion)
                .HasForeignKey(m => m.SolicitudDonacionId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(e => e.EstadoReserva)
            .HasConversion(
                v => v.ToString(),  // Guardar como string usando TipoSangre.ToString()
                v => EstadoReserva.CrearDesdeCadena(v)  // Convertir de string a EstadoReserva
            );
        }
    }
}
