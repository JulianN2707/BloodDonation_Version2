using DonacionSangre.Domain.Entities;

namespace DonacionSangre.Domain.Interfaces.MongoRepository
{
    public interface ICentroSaludMongoRepository
    {
        public Task<CentroSalud> GetCentroSaludByIdAsync(Guid id);
    }
}
