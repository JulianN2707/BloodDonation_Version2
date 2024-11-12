using Carter;
using DonacionSangre.Domain.Dtos;
using MediatR;

namespace DonacionSangre.Application.Features.Persona.Command.CrearPersona
{
    public class CrearPersonaEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("crear-persona", async (PersonaRequestDto request, ISender mediator) =>
            {
                var result = await mediator.Send(new CrearPersonaCommand(request.Nombre,
                    request.Apellido,request.Identificacion,request.Correo,request.Grupo,request.FactorRH,
                    request.TipoPersonaId,request.MunicipioId,request.CentroSaludId));
                return Results.Ok(result);
            }).WithTags("Personas");
        }
    }
}
