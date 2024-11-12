using DonacionSangre.Domain.ValueObjects;
using MediatR;

namespace DonacionSangre.Application.Features.Persona.Command.CrearPersona
{
    public class CrearPersonaCommand : IRequest<Guid>
    {
        public  string Nombre { get; set; }
        public  string Apellido { get; set; }
        public  string Identificacion { get; set; }
        public  string Correo { get; set; }
        public  TipoSangre TipoSangre { get; set; }
        public Guid TipoPersonaId { get; set; }
        public Guid MunicipioId { get; set; }
        public Guid? CentroSaludId { get; set; }

        public CrearPersonaCommand(string nombre, string apellido, string identificacion, string correo,
            string grupo, string factorRH, Guid tipoPersonaId, Guid municipioId, Guid? centroSaludId)
        {
            Nombre = nombre;
            Apellido = apellido;
            Identificacion = identificacion;
            Correo = correo;
            TipoSangre = TipoSangre.Crear(grupo, factorRH);
            TipoPersonaId = tipoPersonaId;
            MunicipioId = municipioId;
            CentroSaludId = centroSaludId;
        }
    }
}
