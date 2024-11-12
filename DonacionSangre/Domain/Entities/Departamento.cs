using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DonacionSangre.Domain.Entities
{
    public class Departamento
    {
        [BsonId]  // Indica que esta propiedad se usará como la clave primaria en MongoDB
        [BsonRepresentation(BsonType.String)]  // Convierte el Guid a una cadena en MongoDB
        [BsonElement("_id")]  // Mapea PersonaId al campo _id de MongoDB
        public Guid DepartamentoId { get; set; }
        public required string Nombre { get; set; }
        public virtual ICollection<Municipio> Municipios { get; set; } = [];
    }
}
