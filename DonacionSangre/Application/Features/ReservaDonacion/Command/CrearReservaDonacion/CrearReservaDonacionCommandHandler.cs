using EReservaDonacion = DonacionSangre.Domain.Entities.ReservaDonacion;
using MediatR;
using DonacionSangre.Domain.Interfaces.SqlServerRepository;
using DonacionSangre.Domain.Interfaces;
using DonacionSangre.Domain.Entities;
using DonacionSangre.Infrastructure.Services.Sincronizacion;
using DonacionSangre.Application.Specification;

namespace DonacionSangre.Application.Features.ReservaDonacion.Command.CrearReservaDonacion
{
    public class CrearReservaDonacionCommandHandler(IRepository<EReservaDonacion> reservaDonacionRepository, IReservaDonacionService reservaDonacionService, IRepository<UsuarioDonacion> usuarioDonacionRepository, SynchronizationService synchronizationService) : IRequestHandler<CrearReservaDonacionCommand, Guid>
    {
        public async Task<Guid> Handle(CrearReservaDonacionCommand request, CancellationToken cancellationToken)
        {
            var usuarioDonacion = await usuarioDonacionRepository.FirstOrDefaultAsync(new ObtenerUsuarioDonacionPorPersonaIdSpecification(request.PersonaId), cancellationToken);
            if (usuarioDonacion is null) 
            {
                throw new InvalidOperationException("No se encontro una persona con el id solicitado.");
            }

            var reservaDonacion = await reservaDonacionService.CrearReservaAsync(usuarioDonacion, request.FechaDonacion);
            await reservaDonacionRepository.AddAsync(reservaDonacion);
            await synchronizationService.SyncReservasDonacion();
            return reservaDonacion.ReservaDonacionId;
        }
    }
}
