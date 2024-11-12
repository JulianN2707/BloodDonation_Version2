using Carter;
using DonacionSangre.Application.Common;
using MediatR;

namespace DonacionSangre.Application.Features.Notificaciones.Queries.NotificacionSolicitudDonacion
{
    public class NotificacionSolicitudDonacionEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet($"{Tags.RutaBase}notificacion-solicitud-donacion", async (ISender sender) =>
            {
                var respuesta = await sender.Send(new NotificacionSolicitudDonacionQuery());
                return Results.Ok(respuesta);
            }).WithTags("Notificaciones").AllowAnonymous();
        }
    }
}
