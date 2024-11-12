using System;
using Carter;
using MediatR;

namespace Solicitudes.Application.Features.SolicitudDonante.Command.CrearDonante;

public class CrearDonanteEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/crear-solicitud-donante", async (CrearDonanteCommand command,ISender mediator)=>{
            var result=await mediator.Send(command);
            return Results.Ok(result);
        }).WithTags("Solicitud-Donante");
    }
}
