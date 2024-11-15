

namespace Personas.Domain.Entities;

public class Persona
{
    // [BsonId]  // Indica que esta propiedad se usará como la clave primaria en MongoDB
    // [BsonRepresentation(BsonType.String)]  // Convierte el Guid a una cadena en MongoDB
    // [BsonElement("_id")]  // Mapea PersonaId al campo _id de MongoDB
    public Guid PersonaId { get; set; }
    public Guid? MunicipioDireccionId { get; set; }
    public string NumeroDocumento { get; set; } = null!;
    public DateTime FechaExpedicionDocumento { get; set; }
    public string? PrimerApellido { get; set; }
    public string? PrimerNombre { get; set; }
    public string? SegundoApellido { get; set; }
    public string? SegundoNombre { get; set; }
    public string? CorreoElectronico { get; set; }
    public string? Celular { get; set; }
    public string? Direccion { get; set; }

}
