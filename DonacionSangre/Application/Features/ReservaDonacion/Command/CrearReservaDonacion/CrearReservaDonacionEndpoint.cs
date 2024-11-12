using Carter;
using DonacionSangre.Application.Common;
using MediatR;

namespace DonacionSangre.Application.Features.ReservaDonacion.Command.CrearReservaDonacion
{
    public class CrearReservaDonacionEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost($"{Tags.RutaBase}crear-reserva-donacion", async (CrearReservaDonacionCommand request, ISender sender) => 
            {
                return Results.Ok(await sender.Send(request));
            }).WithTags(Tags.ReservaDonacion.Tag);
        }
    }
}
