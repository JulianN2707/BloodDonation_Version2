using DonacionSangre.Domain.ValueObjects;
using ESolicitudDonacion = DonacionSangre.Domain.Entities.SolicitudDonacion;

namespace DonacionSangre.Domain.Interfaces.MongoRepository
{
    public interface ISolicitudDonacionMongoRepository
    {
        Task UpdateSolicitudesDonacionAsync(List<ESolicitudDonacion> solicitudesDonacion);
        Task<ESolicitudDonacion> GetSolicitudesDonacionByIdAsync(Guid id);
        Task<ESolicitudDonacion> GetSolicitudDonacionByTipoSangreYMunicipio(Guid municipioId, TipoSangre tipoSangre);
    }
}
