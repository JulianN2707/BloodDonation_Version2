using MediatR;
using EDonante = DonacionSangre.Domain.Entities.Test.Donante;

namespace DonacionSangre.Application.Features.Donante.Queries.ObtenerDonantePorId
{
    public class ObtenerDonantePorIdQuery : IRequest<EDonante>
    {
        public ObtenerDonantePorIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
