using ESolicitudDonacion = DonacionSangre.Domain.Entities.SolicitudDonacion;
using EReservaDonacion = DonacionSangre.Domain.Entities.ReservaDonacion;
using DonacionSangre.Domain.Interfaces.MongoRepository;
using DonacionSangre.Domain.Interfaces.SqlServerRepository;

namespace DonacionSangre.Infrastructure.Services.Sincronizacion
{
    public class SynchronizationService
    {
        private readonly ISolicitudDonacionMongoRepository _solicitudDonacionMongoRepository;
        private readonly IRepository<ESolicitudDonacion> _solicitudDonacionRepository;
        private readonly IRepository<EReservaDonacion> _reservaDonacionRepository;
        private readonly IReservaDonacionMongoRepository _reservaDonacionMongoRepository;

        public SynchronizationService(ISolicitudDonacionMongoRepository solicitudDonacionMongoRepository, IRepository<ESolicitudDonacion> solicitudDonacionRepository, IRepository<EReservaDonacion> reservaDonacionRepository, IReservaDonacionMongoRepository reservaDonacionMongoRepository)
        {
            _solicitudDonacionMongoRepository = solicitudDonacionMongoRepository;
            _solicitudDonacionRepository = solicitudDonacionRepository;
            _reservaDonacionRepository = reservaDonacionRepository;
            _reservaDonacionMongoRepository = reservaDonacionMongoRepository;
        }

        public async Task SyncSolicitudesDonacion()
        {
            var solicitudesDonacion = await _solicitudDonacionRepository.ListAsync();
            await _solicitudDonacionMongoRepository.UpdateSolicitudesDonacionAsync(solicitudesDonacion);
        }

        public async Task SyncReservasDonacion()
        {
            var reservasDonacion = await _reservaDonacionRepository.ListAsync();
            await _reservaDonacionMongoRepository.UpdateReservasDonacionAsync(reservasDonacion);
        }
    }
}
