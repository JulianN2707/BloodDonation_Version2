using DonacionSangre.Domain.ValueObjects;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DonacionSangre.Domain.Entities
{
    public class SolicitudDonacion
    {
        [BsonId]  // Indica que esta propiedad se usará como la clave primaria en MongoDB
        [BsonRepresentation(BsonType.String)]  // Convierte el Guid a una cadena en MongoDB
        [BsonElement("_id")]  // Mapea PersonaId al campo _id de MongoDB
        public Guid SolicitudDonacionId { get; private set; }
        public DateTime FechaSolicitud { get; private set; }
        public Guid CentroSaludId { get; private set; }
        public  TipoSangre TipoSangre {  get; private set; }
        public virtual CentroSalud CentroSalud { get; private set; } = null!;
        public  EstadoSolicitudDonacion Estado { get; private set; }
        public virtual ICollection<ReservaDonacion> ReservasDonacion { get; private set; } = [];

        public static SolicitudDonacion Crear(Guid centroSaludId, TipoSangre tipoSangre)
        {
            return new SolicitudDonacion
            {
                SolicitudDonacionId = Guid.NewGuid(),
                FechaSolicitud = DateTime.UtcNow, 
                CentroSaludId = centroSaludId,
                TipoSangre = tipoSangre,
                Estado = EstadoSolicitudDonacion.Activo()
            };
        }
    }
}
