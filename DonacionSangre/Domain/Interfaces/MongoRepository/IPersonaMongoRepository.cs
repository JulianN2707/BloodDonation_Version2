using DonacionSangre.Domain.Entities;
using EDonante = DonacionSangre.Domain.Entities.Test.Donante;

namespace DonacionSangre.Domain.Interfaces.MongoRepository
{
    public interface IPersonaMongoRepository
    {
        Task UpdatePersonaAsync(List<Persona> personas);
        Task<Persona?> GetPersonaByIdAsync(Guid id);
        Task<List<Persona>> GetPersonasAsync();
    }
}
