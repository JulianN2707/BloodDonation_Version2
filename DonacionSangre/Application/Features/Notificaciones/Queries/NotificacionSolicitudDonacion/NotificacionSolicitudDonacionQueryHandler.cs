using DonacionSangre.Domain.Dtos;
using DonacionSangre.Infrastructure.Services.NotificacionesAutomaticas;
using MediatR;

namespace DonacionSangre.Application.Features.Notificaciones.Queries.NotificacionSolicitudDonacion
{
    public class NotificacionSolicitudDonacionQueryHandler : IRequestHandler<NotificacionSolicitudDonacionQuery, Response<IEnumerable<Recipient>>>
    {
        private readonly INotificacionAutomaticaService _notificacionAutomaticaService;

        public NotificacionSolicitudDonacionQueryHandler(INotificacionAutomaticaService notificacionAutomaticaService)
        {
            _notificacionAutomaticaService = notificacionAutomaticaService;
        }

        public async Task<Response<IEnumerable<Recipient>>> Handle(NotificacionSolicitudDonacionQuery request, CancellationToken cancellationToken)
        {
            var notificaciones = await _notificacionAutomaticaService.NotificarSolicitudesDonacion();
            return new Response<IEnumerable<Recipient>>(notificaciones);
        }
    }
}
