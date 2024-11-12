using DonacionSangre.Domain.Interfaces.SqlServerRepository;
using DonacionSangre.Infrastructure.Services.Sincronizacion;
using MediatR;
using EPersona = DonacionSangre.Domain.Entities.Persona;

namespace DonacionSangre.Application.Features.Persona.Command.CrearPersona
{
    public class CrearPersonaCommandHandler : IRequestHandler<CrearPersonaCommand, Guid>
    {
        private readonly IRepository<EPersona> _personaRepository;
        private readonly SynchronizationService _synchronizationService;

        public CrearPersonaCommandHandler(IRepository<EPersona> personaRepository, SynchronizationService synchronizationService)
        {
            _personaRepository = personaRepository;
            _synchronizationService = synchronizationService;
        }

        public async Task<Guid> Handle(CrearPersonaCommand request, CancellationToken cancellationToken)
        {
            var persona = await EPersona.CrearPersona(request);
            await _personaRepository.AddAsync(persona);
            await _synchronizationService.SyncPersonas();
            return persona.PersonaId;

        }
    }
}
