using Carter;
using DonacionSangre.Application.Features.Donante.Queries.ObtenerDonantePorId;
using MediatR;

namespace DonacionSangre.Application.Features.Persona.Queries.ObtenerPersonaPorId
{
    public class ObtenerPersonaPorIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("obtener-persona-por-id/{id:guid}", async (Guid id, ISender mediator) =>
            {
                var query = new ObtenerPersonaPorIdQuery(id);
                var donante = await mediator.Send(query);
                if (donante == null)
                {
                    return Results.NotFound(new { Message = "Persona no encontrado." });
                }
                return Results.Ok(donante);

            }).WithTags("Personas");
        }
    }
}
