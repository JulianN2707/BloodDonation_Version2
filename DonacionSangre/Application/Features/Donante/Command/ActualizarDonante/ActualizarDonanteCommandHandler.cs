using DonacionSangre.Infrastructure.Services.Sincronizacion;
using DonacionSangre.Infrastructure.SqlServerRepositories.Donante;
using MediatR;

namespace DonacionSangre.Application.Features.Donante.Command.ActualizarDonante
{
    public class ActualizarDonanteCommandHandler : IRequestHandler<ActualizarDonanteCommand, Guid>
    {
        private readonly IDonanteRepository _donanteRepository;
        private readonly SynchronizationService _synchronizationService;

        public ActualizarDonanteCommandHandler(IDonanteRepository donanteRepository, SynchronizationService synchronizationService)
        {
            _donanteRepository = donanteRepository;
            _synchronizationService = synchronizationService;
        }

        public async Task<Guid> Handle(ActualizarDonanteCommand request, CancellationToken cancellationToken)
        {
            // Obtener el donante existente desde SQL Server
            var donante = await _donanteRepository.GetByIdAsync(request.Id);
            if (donante == null)
            {
                throw new Exception("Donante no encontrado");
            }

            // Actualizar los datos del donante
            donante.Nombre = request.Nombre;
            donante.TipoSangre = request.TipoSangre;
            donante.ZonaResidencia = request.ZonaResidencia;

            await _donanteRepository.UpdateAsync(donante);
            await _synchronizationService.SyncDonantes();
            return donante.Id;
        }
    }
}
