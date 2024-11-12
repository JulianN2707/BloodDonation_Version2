using Carter;
using MediatR;

namespace DonacionSangre.Application.Features.Donante.Queries.ObtenerDonantePorId
{
    public class ObtenerDonantePorIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/obtener-donante-por-id/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var query = new ObtenerDonantePorIdQuery(id);
                var donante = await mediator.Send(query);
                if (donante == null)
                {
                    return Results.NotFound(new { Message = "Donante no encontrado." });
                }
                return Results.Ok(donante);
            }).WithTags("Donante");
       
        }
    }
}
