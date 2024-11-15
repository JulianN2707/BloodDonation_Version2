using DonacionSangre.Domain.ValueObjects;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DonacionSangre.Domain.Entities
{
    public class ReservaDonacion
    {
        [BsonId]  // Indica que esta propiedad se usará como la clave primaria en MongoDB
        [BsonRepresentation(BsonType.String)]  // Convierte el Guid a una cadena en MongoDB
        [BsonElement("_id")]  // Mapea PersonaId al campo _id de MongoDB
        public Guid ReservaDonacionId { get; private set; }
        public DateTime FechaReserva { get; private set; }
        public Guid PersonaId { get; private set; }
        public EstadoReserva EstadoReserva { get; private set; } 
        public Guid SolicitudDonacionId { get; private set; }
        public virtual SolicitudDonacion SolicitudDonacion { get; set; } = null!;

        public ReservaDonacion() { }

        public ReservaDonacion(DateTime fechaReserva, Guid personaId, Guid solicitudDonacionId, EstadoReserva estadoReserva)
        {
            ReservaDonacionId = Guid.NewGuid();
            FechaReserva = fechaReserva;
            PersonaId = personaId;
            SolicitudDonacionId = solicitudDonacionId;
            EstadoReserva = estadoReserva;
        }

        public void CambiarEstado(EstadoReserva nuevoEstado)
        {
            EstadoReserva = nuevoEstado;
        }

    }
}
