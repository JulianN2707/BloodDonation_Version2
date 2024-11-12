using System;
using MediatR;
using Solicitudes.Domain.Dto;

namespace Solicitudes.Application.Features.SolicitudDonante.Command.CrearDonante;

public class CrearDonanteCommand : IRequest<CrearDonanteResponse>
{
    public string NumeroDocumento { get; set; } = null!;
    public DateTime FechaExpedicionDocumento { get; set; }
    public string? PrimerApellido { get; set; }
    public string? PrimerNombre { get; set; }
    public string? SegundoApellido { get; set; }
    public string? SegundoNombre { get; set; }
    public string? CorreoElectronico { get; set; }
    public string? Celular { get; set; }
    public string? Direccion { get; set; }
    public int? MunicipioDireccionId { get; set; }
    public int TipoCargoPersonaId { get; set; }
    public List<CrearArchivoDto> Archivos { get; set; }

}
