using DonacionSangre.Domain.Entities;
using EDonante = DonacionSangre.Domain.Entities.Test.Donante;

namespace DonacionSangre.Domain.Interfaces.MongoRepository
{
    public interface ICentroSaludMongoRepository
    {
        public Task<CentroSalud> GetCentroSaludByIdAsync(Guid id);
    }
}
