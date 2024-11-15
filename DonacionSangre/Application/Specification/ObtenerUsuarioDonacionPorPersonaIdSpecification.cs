using Ardalis.Specification;
using DonacionSangre.Domain.Entities;

namespace DonacionSangre.Application.Specification
{
    public class ObtenerUsuarioDonacionPorPersonaIdSpecification : Specification<UsuarioDonacion>
    {
        public ObtenerUsuarioDonacionPorPersonaIdSpecification(Guid personaId) 
        {
            Query.Where(x => x.PersonaId == personaId);
        }
    }
}