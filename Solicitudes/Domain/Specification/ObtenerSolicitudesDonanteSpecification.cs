using Ardalis.Specification;
using Solicitudes.Domain.Entities;

namespace Solicitudes.Domain.Specification
{
    public class ObtenerSolicitudesDonanteSpecification : Specification<SolicitudUsuario>
    {
        public ObtenerSolicitudesDonanteSpecification()
        {
            Query.Include(x => x.EstadoSolicitudUsuario);
        }
    }
}
