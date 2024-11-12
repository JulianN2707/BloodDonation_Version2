using EReservaDonacion = DonacionSangre.Domain.Entities.ReservaDonacion;
using MediatR;
using DonacionSangre.Infrastructure.Services.Sincronizacion;
using DonacionSangre.Domain.Interfaces.MongoRepository;
using DonacionSangre.Domain.Interfaces.SqlServerRepository;
using DonacionSangre.Domain.Interfaces;

namespace DonacionSangre.Application.Features.ReservaDonacion.Command.CrearReservaDonacion
{
    public class CrearReservaDonacionCommandHandler(IRepository<EReservaDonacion> reservaDonacionRepository, IReservaDonacionService reservaDonacionService, IPersonaMongoRepository personaMongoRepository, SynchronizationService synchronizationService) : IRequestHandler<CrearReservaDonacionCommand, Guid>
    {
        public async Task<Guid> Handle(CrearReservaDonacionCommand request, CancellationToken cancellationToken)
        {
            var persona = await personaMongoRepository.GetPersonaByIdAsync(request.PersonaId);
            if (persona is null) 
            {
                throw new InvalidOperationException("No se encontro una persona con el id solicitado.");
            }

            var reservaDonacion = await reservaDonacionService.CrearReservaAsync(persona, request.FechaDonacion);
            await reservaDonacionRepository.AddAsync(reservaDonacion);
            await synchronizationService.SyncReservasDonacion();
            return reservaDonacion.ReservaDonacionId;
        }
    }
}
