using System;
using MediatR;

namespace Solicitudes.Application.Features.SolicitudDonante.Command.AprobarDonante;

public class AprobarDonanteCommandHandler : IRequestHandler<AprobarDonanteCommand, bool>
{
    public Task<bool> Handle(AprobarDonanteCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
