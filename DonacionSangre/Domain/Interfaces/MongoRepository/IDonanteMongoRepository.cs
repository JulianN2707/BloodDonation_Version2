using EDonante = DonacionSangre.Domain.Entities.Test.Donante;

namespace DonacionSangre.Domain.Interfaces.MongoRepository
{
    public interface IDonanteMongoRepository
    {
        Task UpdateDonantesAsync(List<EDonante> donantes);
        Task<EDonante> GetDonanteByIdAsync(Guid id);
    }
}
