using Carter;
using DonacionSangre.Application.Common;
using MediatR;

namespace DonacionSangre.Application.Features.SolicitudDonacion.Command.CrearSolicitudDonacion
{
    public class CrearSolicitudDonacionEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost($"{Tags.RutaBase}crear-solicitud-donacion", async (CrearSolicitudDonacionCommand command, ISender mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Ok(result);
            }).WithTags(Tags.SolicitudDonacion.Tag);
        }
    }
}
