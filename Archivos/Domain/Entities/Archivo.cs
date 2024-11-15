using System;

namespace Archivos.Domain.Entities;

public class Archivo
{
    public Guid ArchivoId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Ruta { get; set; } = string.Empty;
    public Guid TipoArchivoId { get; set; }
    public string? Extension { get; set; }
    public DateTime? FechaCreacion { get; set; }
}
