using Carter;
using MediatR;
using Personas.Application.Features.Persona.Queries.ObtenerPersonaPorId;

namespace Personas.Application.Features.Personas.Queries.ObtenerPersonaPorId
{
    public class ObtenerPersonaPorIdEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/v1/obtener-personaid/{personaId}", async (int personaId, ISender sender) =>
            {
                var result = await sender.Send(new ObtenerPersonaPorIdQuery(personaId));
                return Results.Ok(result);
            }).WithTags("Persona");
        }
    }
}
