using DonacionSangre.Domain.Entities;
using DonacionSangre.Domain.Interfaces;
using DonacionSangre.Domain.Interfaces.MongoRepository;
using DonacionSangre.Domain.ValueObjects;

namespace DonacionSangre.Domain.Services
{
    public class ReservaDonacionService(ISolicitudDonacionMongoRepository solicitudDonacionRepository) : IReservaDonacionService
    {
        public async Task<ReservaDonacion> CrearReservaAsync(Persona persona, DateTime fechaReserva)
        {
            var solicitudDonacion = await ObtenerSolicitudDonacionAsync(persona);
            return new ReservaDonacion(fechaReserva, persona.PersonaId, solicitudDonacion.SolicitudDonacionId, EstadoReserva.Confirmada()); 
        }

        private async Task<SolicitudDonacion> ObtenerSolicitudDonacionAsync(Persona persona)
        {
            var solicitudDonacion = await solicitudDonacionRepository.GetSolicitudDonacionByTipoSangreYMunicipio(persona.MunicipioId, persona.TipoSangre);

            if (solicitudDonacion is null)
            {
                throw new InvalidOperationException("No hay ninguna solicitud válida para esta reserva.");
            }

            return solicitudDonacion;
        }
    }
}
