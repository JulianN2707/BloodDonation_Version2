using Carter;
using MediatR;

namespace DonacionSangre.Application.Features.Donante.Command.ActualizarDonante
{
    public class ActualizarDonanteEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/actualizar-donante-por-guid", async ( ActualizarDonanteCommand command, IMediator mediator) =>
            {
                var resultado = await mediator.Send(command);
                return Results.Ok(new { Id = resultado, Message = "Donante actualizado y sincronizado con MongoDB." });
            })
        .WithTags("Donantes");
        }
    }
}
