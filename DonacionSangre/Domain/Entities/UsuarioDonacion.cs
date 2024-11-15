using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using DonacionSangre.Domain.ValueObjects;

namespace DonacionSangre.Domain.Entities
{
    public class UsuarioDonacion
    {
        [BsonId]  // Indica que esta propiedad se usará como la clave primaria en MongoDB
        [BsonRepresentation(BsonType.String)]  // Convierte el Guid a una cadena en MongoDB
        [BsonElement("_id")]  // Mapea PersonaId al campo _id de MongoDB
        public Guid UsuarioDonacionId { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid PersonaId {  get; set; }
        public Guid MunicipioId {  get; set; }
        public TipoSangre TipoSangre { get; set; }
        public required string PrimerApellido { get; set; }
        public required string PrimerNombre { get; set; }
        public required string CorreoElectronico { get; set; }
        public required string Cargo {  get; set; }
        public string? Direccion {  get; set; }
        public string Celular {  get; set; }
    }
}