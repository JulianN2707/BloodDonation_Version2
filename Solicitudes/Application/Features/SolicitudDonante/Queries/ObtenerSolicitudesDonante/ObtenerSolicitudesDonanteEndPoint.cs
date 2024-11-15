using Carter;
using MediatR;

namespace Solicitudes.Application.Features.SolicitudDonante.Queries.ObtenerSolicitudesDonante
{
    public class ObtenerSolicitudesDonanteEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/v1/obtener-solicitudesdonante", async (ISender sender) =>
            {
                var result = await sender.Send(new ObtenerSolicitudesDonanteQuery());
                return Results.Ok(result);
            }).WithTags("Solicitud Donante");
        }
    }
}
