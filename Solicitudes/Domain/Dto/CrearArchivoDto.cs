using System;

namespace Solicitudes.Domain.Dto;

public record struct CrearArchivoDto(IFormFile Archivo,
        string TipoArchivoId,
        string? ReferenciaSolicitud);
