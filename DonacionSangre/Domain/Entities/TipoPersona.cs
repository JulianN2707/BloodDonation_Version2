using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DonacionSangre.Domain.Entities
{
    public class TipoPersona
    {
        [BsonId]  // Indica que esta propiedad se usará como la clave primaria en MongoDB
        [BsonRepresentation(BsonType.String)]  // Convierte el Guid a una cadena en MongoDB
        [BsonElement("_id")]  // Mapea PersonaId al campo _id de MongoDB
        public Guid TipoPersonaId { get; set; }
        public required string Descripcion { get; set; }
        public virtual ICollection<Persona> Personas { get; set; } = [];
    }
}
