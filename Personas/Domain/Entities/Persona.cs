using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Personas.Domain.ValueObjects;

namespace Personas.Domain.Entities
{
    public class Persona
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        [BsonElement("_id")]
        public Guid PersonaId { get; private set; }
        public Guid? MunicipioDireccionId { get; set; }
        public required string NumeroDocumento { get; set; }
        public DateTime FechaExpedicionDocumento { get; set; }
        public string? PrimerApellido { get; set; }
        public string? PrimerNombre { get; set; }
        public string? SegundoApellido { get; set; }
        public string? SegundoNombre { get; set; }
        public string? CorreoElectronico { get; set; }
        public string? Celular { get; set; }
        public string? Direccion { get; set; }
        public Guid TipoPersonaId { get; private set; }
        public TipoSangre TipoSangre { get; private set; } = null!;
        public virtual TipoPersona TipoPersona { get; set; } = null!;
    }
}