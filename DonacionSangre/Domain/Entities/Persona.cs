using DonacionSangre.Application.Features.Persona.Command.CrearPersona;
using DonacionSangre.Domain.Enums;
using DonacionSangre.Domain.ValueObjects;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DonacionSangre.Domain.Entities
{
    public class Persona
    {
        [BsonId]  
        [BsonRepresentation(BsonType.String)]  
        [BsonElement("_id")] 
        public Guid PersonaId { get; private set; }
        public  string Nombre { get; private set; }
        public  string Apellido { get; private set; }
        public  string Identificacion { get; private set; }
        public  string Correo { get; private set; }
        public  TipoSangre TipoSangre { get; private set; }
        public Guid TipoPersonaId { get; private set; }
        public virtual TipoPersona TipoPersona { get; set; } = null!;
        public Guid MunicipioId { get; private set; }
        public virtual Municipio Municipio { get; set; } = null!;
        public virtual ICollection<ReservaDonacion> ReservasDonacion { get; set; } = [];
        public Guid? CentroSaludId { get; private set; }
        public virtual CentroSalud? CentroSalud { get; set; }


        public async static Task<Persona> CrearPersona(CrearPersonaCommand command)
        {
            if(TipoPersonaConst.Enfermero.Equals(command.TipoPersonaId.ToString(),StringComparison.OrdinalIgnoreCase)) {
                if (string.IsNullOrEmpty(command.CentroSaludId.ToString()))
                {
                    throw new Exception("Error, Si el tipo de persona es enfermero, el centro de salud es obligatorio");
                }
            }
            return new Persona
            {
                Apellido = command.Apellido,
                Correo = command.Correo,
                CentroSaludId = TipoPersonaConst.Enfermero.Equals(command.TipoPersonaId.ToString(), StringComparison.OrdinalIgnoreCase) == true ? command.CentroSaludId : null,
                Identificacion = command.Identificacion,
                MunicipioId = command.MunicipioId,
                Nombre = command.Nombre,
                TipoPersonaId = command.TipoPersonaId,
                TipoSangre = command.TipoSangre,
            };
        }

    }
}
