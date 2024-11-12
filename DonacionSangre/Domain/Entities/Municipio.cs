using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DonacionSangre.Domain.Entities
{
    public class Municipio
    {
        [BsonId]  // Indica que esta propiedad se usará como la clave primaria en MongoDB
        [BsonRepresentation(BsonType.String)]  // Convierte el Guid a una cadena en MongoDB
        [BsonElement("_id")]  // Mapea PersonaId al campo _id de MongoDB
        public Guid MunicipioId { get; set; }
        public required string Nombre { get; set; }
        public Guid DepartamentoId { get; set; }
        public virtual Departamento Departamento { get; set; } = null!;
        public virtual ICollection<Persona> Personas { get; set; } = [];
        public virtual ICollection<CentroSalud> CentrosSalud { get; set; } = [];
    }
}
