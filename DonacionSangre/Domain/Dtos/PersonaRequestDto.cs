
namespace DonacionSangre.Domain.Dtos
{
    public class PersonaRequestDto
    {
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string Identificacion { get; set; }
        public required string Correo { get; set; }
        public required string Grupo { get; set; }
        public required string FactorRH { get; set; }
        public Guid TipoPersonaId { get; set; }
        public Guid MunicipioId { get; set; }
        public Guid? CentroSaludId { get; set; }
    }
}
