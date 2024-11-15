using System;

namespace Solicitudes.Domain.Dto;

public record struct CrearArchivoDto(IFormFile Archivo,
        Guid TipoArchivoId);
