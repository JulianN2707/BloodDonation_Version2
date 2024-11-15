using DonacionSangre.Domain.Entities;
using DonacionSangre.Domain.Interfaces;
using DonacionSangre.Domain.Interfaces.MongoRepository;
using DonacionSangre.Domain.ValueObjects;

namespace DonacionSangre.Domain.Services
{
    public class ReservaDonacionService(ISolicitudDonacionMongoRepository solicitudDonacionRepository) : IReservaDonacionService
    {
        public async Task<ReservaDonacion> CrearReservaAsync(UsuarioDonacion usuarioDonacion, DateTime fechaReserva)
        {
            var solicitudDonacion = await ObtenerSolicitudDonacionAsync(usuarioDonacion);
            return new ReservaDonacion(fechaReserva, usuarioDonacion.PersonaId, solicitudDonacion.SolicitudDonacionId, EstadoReserva.Confirmada()); 
        }

        private async Task<SolicitudDonacion> ObtenerSolicitudDonacionAsync(UsuarioDonacion usuarioDonacion)
        {
            var solicitudDonacion = await solicitudDonacionRepository.GetSolicitudDonacionByTipoSangreYMunicipio(usuarioDonacion.MunicipioId, usuarioDonacion.TipoSangre);

            if (solicitudDonacion is null)
            {
                throw new InvalidOperationException("No hay ninguna solicitud válida para esta reserva.");
            }

            return solicitudDonacion;
        }
    }
}
