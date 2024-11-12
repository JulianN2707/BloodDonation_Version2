using DonacionSangre.Domain.Interfaces.MongoRepository;
using MediatR;
using EPersona = DonacionSangre.Domain.Entities.Persona;
namespace DonacionSangre.Application.Features.Persona.Queries.ObtenerPersonaPorId
{
    public class ObtenerPersonaPorIdQueryHandler : IRequestHandler<ObtenerPersonaPorIdQuery, EPersona>
    {
        private readonly IPersonaMongoRepository _personaMongoRepository;

        public ObtenerPersonaPorIdQueryHandler(IPersonaMongoRepository personaMongoRepository)
        {
            _personaMongoRepository = personaMongoRepository;
        }

        public async Task<EPersona> Handle(ObtenerPersonaPorIdQuery request, CancellationToken cancellationToken)
        {
            var persona = await _personaMongoRepository.GetPersonaByIdAsync(request.Id);
            return persona;
        }
    }
}
