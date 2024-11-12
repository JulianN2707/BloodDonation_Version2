using DonacionSangre.Domain.Interfaces.MongoRepository;
using MediatR;
using EDonante = DonacionSangre.Domain.Entities.Test.Donante;

namespace DonacionSangre.Application.Features.Donante.Queries.ObtenerDonantePorId
{
    public class ObtenerDonantePorIdQueryHandler : IRequestHandler<ObtenerDonantePorIdQuery, EDonante>
    {
        private readonly IDonanteMongoRepository _mongoDbRepository;

        public ObtenerDonantePorIdQueryHandler(IDonanteMongoRepository mongoDbRepository)
        {
            _mongoDbRepository = mongoDbRepository;
        }

        public async Task<EDonante> Handle(ObtenerDonantePorIdQuery request, CancellationToken cancellationToken)
        {
            var donante = await _mongoDbRepository.GetDonanteByIdAsync(request.Id);
            return donante;
        }
    }
}
