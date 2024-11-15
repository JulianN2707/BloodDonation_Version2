using Ardalis.GuardClauses;
using MediatR;
using Personas.Domain.Dto;

namespace Personas.Application.Features.Personas.Queries.ObtenerPersonaPorId
{
    public class ObtenerPersonaPorIdQuery : IRequest<ObtenerPersonaPorIdResponse>
    {
        public int PersonaId { get; set; }

        public ObtenerPersonaPorIdQuery(int personaId)
        {
            PersonaId = Guard.Against.NegativeOrZero(personaId, nameof(personaId));
        }
    }
}
