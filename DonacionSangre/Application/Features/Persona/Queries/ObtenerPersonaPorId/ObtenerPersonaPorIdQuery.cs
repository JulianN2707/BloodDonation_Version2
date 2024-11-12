using MediatR;
using EPersona = DonacionSangre.Domain.Entities.Persona;
namespace DonacionSangre.Application.Features.Persona.Queries.ObtenerPersonaPorId
{
    public class ObtenerPersonaPorIdQuery : IRequest<EPersona>
    {
        public ObtenerPersonaPorIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
