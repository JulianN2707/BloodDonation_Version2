using DonacionSangre.Infrastructure.Services.Sincronizacion;
using DonacionSangre.Infrastructure.SqlServerRepositories.Donante;
using MediatR;
using EDonante = DonacionSangre.Domain.Entities.Test.Donante;

namespace DonacionSangre.Application.Features.Donante.Command.RegistrarDonante
{
    public class RegistrarDonanteCommandHandler : IRequestHandler<RegistrarDonanteCommand, Guid>
    {
        private readonly IDonanteRepository _repository;
        private readonly SynchronizationService _synchronizationService;

        public RegistrarDonanteCommandHandler(IDonanteRepository repository, SynchronizationService synchronizationService)
        {
            _repository = repository;
            _synchronizationService = synchronizationService;
        }

        public async Task<Guid> Handle(RegistrarDonanteCommand request, CancellationToken cancellationToken)
        {
            var donante = new EDonante(request.Nombre, request.TipoSangre, request.ZonaResidencia);
            await _repository.AddAsync(donante);
            await _synchronizationService.SyncDonantes();
            return donante.Id;
        }
    }
}
