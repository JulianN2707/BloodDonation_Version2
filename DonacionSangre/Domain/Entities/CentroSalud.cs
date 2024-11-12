using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DonacionSangre.Domain.Entities
{
    public class CentroSalud
    {
        [BsonId]  // Indica que esta propiedad se usará como la clave primaria en MongoDB
        [BsonRepresentation(BsonType.String)]  // Convierte el Guid a una cadena en MongoDB
        [BsonElement("_id")]  // Mapea PersonaId al campo _id de MongoDB
        public Guid CentroSaludId { get; set; }
        public required string Nombre { get; set; }
        public required string Direccion { get; set; } //value object, cambiar
        public Guid MunicipioId { get; set; }
        public virtual Municipio Municipio { get; set; } = null!;
        [BsonIgnore]
        public virtual ICollection<SolicitudDonacion> SolicitudesDonacion { get; set; } = [];
        public virtual ICollection<Persona> Personas { get; set; } = [];
    }
}