using System;
using Carter;
using MediatR;

namespace Solicitudes.Application.Features.SolicitudDonante.Command.AprobarDonante;

public class AprobarDonanteEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/aprobar-donante", async (AprobarDonanteCommand command,ISender mediator)=>{
            var result=await mediator.Send(command);
            return Results.Ok(result);
        }).WithTags("Donante");
    }
}
