using DonacionSangre.Domain.Dtos;

namespace DonacionSangre.Infrastructure.Services.NotificacionesAutomaticas
{
    public interface INotificacionAutomaticaService
    {
        public Task<List<Recipient>> NotificarSolicitudesDonacion();
    }
}
