using Carter;
using MediatR;

namespace DonacionSangre.Application.Features.Donante.Command.RegistrarDonante
{
    public class RegistrarDonanteEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/registrar-donante", async (RegistrarDonanteCommand command, ISender mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Ok(result);
            }).WithTags("Donante");
        }
    }
}
