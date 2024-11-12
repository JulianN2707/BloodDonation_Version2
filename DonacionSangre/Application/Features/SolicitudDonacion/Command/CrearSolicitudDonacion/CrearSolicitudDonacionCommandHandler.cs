
using DonacionSangre.Infrastructure.Services.Sincronizacion;
using MediatR;
using ESolicitudDonacion = DonacionSangre.Domain.Entities.SolicitudDonacion;
using ECentroSalud = DonacionSangre.Domain.Entities.CentroSalud;
using DonacionSangre.Domain.ValueObjects;
using DonacionSangre.Domain.Interfaces.MongoRepository;
using DonacionSangre.Domain.Interfaces.SqlServerRepository;

namespace DonacionSangre.Application.Features.SolicitudDonacion.Command.CrearSolicitudDonacion
{
    public class CrearSolicitudDonacionCommandHandler : IRequestHandler<CrearSolicitudDonacionCommand, Guid>
    {
        private readonly SynchronizationService _synchronizationService;
        private readonly IRepository<ESolicitudDonacion> _repositorySolicitudDonacion;
        private readonly IRepository<ECentroSalud> _repositoryCentroSalud;
        private readonly ICentroSaludMongoRepository _centroSaludMongoRepository;

        public CrearSolicitudDonacionCommandHandler(SynchronizationService synchronizationService, IRepository<ESolicitudDonacion> repositorySolicitudLicencia,
            IRepository<ECentroSalud> repositoryCentroSalud, ICentroSaludMongoRepository centroSaludMongoRepository)
        {
            _synchronizationService = synchronizationService;
            _repositorySolicitudDonacion = repositorySolicitudLicencia;
            _repositoryCentroSalud = repositoryCentroSalud;
            _centroSaludMongoRepository = centroSaludMongoRepository;
        }

        public async Task<Guid> Handle(CrearSolicitudDonacionCommand request, CancellationToken cancellationToken)
        {
            var centroSalud = await _repositoryCentroSalud.GetByIdAsync(request.CentroSaludId) ?? throw new Exception("Error, centro de salud no encontrado");
            var solicitudDonacion = ESolicitudDonacion.Crear(centroSalud.CentroSaludId, TipoSangre.Crear(request.GrupoSanguineo, request.Rh));
            await _repositorySolicitudDonacion.AddAsync(solicitudDonacion);
            await _synchronizationService.SyncSolicitudesDonacion();

            return solicitudDonacion.SolicitudDonacionId;
        }
    }
}
