using System;
using Carter;
using MediatR;
using Solicitudes.Domain.Dto;

namespace Solicitudes.Application.Features.SolicitudDonante.Command.CrearDonante;

public class CrearDonanteEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/crear-solicitud-donante", async (HttpContext context,ISender mediator)=>{
            // Leer los datos del formulario enviados como FormData
        var form = await context.Request.ReadFormAsync();
        
        // Construir el objeto CrearSolicitudCuradorCommand desde los datos del formulario
        var model = new CrearDonanteCommand
        {
            // Campos del formulario
            NumeroDocumento = form["numeroDocumento"],
            FechaExpedicionDocumento = string.IsNullOrEmpty(form["fechaExpedicionDocumento"]) ? DateTime.Now : DateTime.Parse(form["fechaExpedicionDocumento"]),
            PrimerApellido = form["primerApellido"],
            PrimerNombre = form["primerNombre"],
            SegundoApellido = form["segundoApellido"],
            SegundoNombre = form["segundoNombre"],
            CorreoElectronico = form["correoElectronico"],
            Celular = form["celular"],
            Direccion = form["direccion"],
            MunicipioDireccionId = string.IsNullOrEmpty(form["municipioDireccionId"]) ? null : Guid.Parse(form["municipioDireccionId"]),
            TipoPersonaId = Guid.Parse(form["tipoPersonaId"]),
            GrupoSanguineo = form["grupoSanguineo"],
            FactorRh = form["factorRh"],
            
            // Manejo de archivos (Archivos pueden venir como FormData)
            Archivos = form.Files.Select((file, index) => new CrearArchivoDto
            {
                Archivo = file,
                TipoArchivoId = Guid.Parse(form[$"archivos[{index}].tipoArchivoId"]!) // Se obtiene el tipo de archivo relacionado
            }).ToList()
        };

        // Enviar el modelo al handler mediante MediatR
        var result = await mediator.Send(model);

        // Devolver el resultado
        return Results.Ok(result);
        }).WithTags("Solicitud-Donante");
    }
}
