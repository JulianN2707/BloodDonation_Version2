using DonacionSangre.Domain.Entities;
using DonacionSangre.Infrastructure.SqlServerRepositories.Donante;
using DonacionSangre.Infrastructure.SqlServerRepositories.Reserva;
using ESolicitudDonacion = DonacionSangre.Domain.Entities.SolicitudDonacion;
using EReservaDonacion = DonacionSangre.Domain.Entities.ReservaDonacion;
using EPersona = DonacionSangre.Domain.Entities.Persona;
using static DonacionSangre.Application.Common.Tags;
using DonacionSangre.Domain.Interfaces.MongoRepository;
using DonacionSangre.Domain.Interfaces.SqlServerRepository;

namespace DonacionSangre.Infrastructure.Services.Sincronizacion
{
    public class SynchronizationService
    {
        private readonly IDonanteMongoRepository _mongoRepository;
        private readonly ISolicitudDonacionMongoRepository _solicitudDonacionMongoRepository;
        private readonly IRepository<ESolicitudDonacion> _solicitudDonacionRepository;
        private readonly IRepository<EReservaDonacion> _reservaDonacionRepository;
        private readonly IRepository<EPersona> _personaRepository;
        private readonly IReservaDonacionMongoRepository _reservaDonacionMongoRepository;
        private readonly IPersonaMongoRepository _personaMongoRepository;
        private readonly IDonanteRepository _donanteRepository;
        private readonly IReservaRepository _reservaRepository;

        public SynchronizationService(IDonanteMongoRepository mongoRepository, ISolicitudDonacionMongoRepository solicitudDonacionMongoRepository, IRepository<ESolicitudDonacion> solicitudDonacionRepository, IRepository<EReservaDonacion> reservaDonacionRepository, IRepository<EPersona> personaRepository, IReservaDonacionMongoRepository reservaDonacionMongoRepository,
            IDonanteRepository donanteRepository, IReservaRepository reservaRepository, IPersonaMongoRepository personaMongoRepository)
        {
            _mongoRepository = mongoRepository;
            _solicitudDonacionMongoRepository = solicitudDonacionMongoRepository;
            _solicitudDonacionRepository = solicitudDonacionRepository;
            _reservaDonacionRepository = reservaDonacionRepository;
            _personaRepository = personaRepository;
            _reservaDonacionMongoRepository = reservaDonacionMongoRepository;
            _donanteRepository = donanteRepository;
            _reservaRepository = reservaRepository;
            _personaMongoRepository = personaMongoRepository;
        }

        // Sincroniza todos los Donantes con MongoDB
        public async Task SyncDonantes()
        {
            var donantes = await _donanteRepository.GetAllAsync();
            await _mongoRepository.UpdateDonantesAsync(donantes);
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

        public async Task SyncPersonas()
        {
            var personas = await _personaRepository.ListAsync();
            await _personaMongoRepository.UpdatePersonaAsync(personas);
        }
    }
}
