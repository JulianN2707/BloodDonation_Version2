using System;
using MediatR;

namespace Solicitudes.Application.Features.SolicitudDonante.Command.AprobarDonante;

public class AprobarDonanteCommand : IRequest<bool>
{
    public Guid SolicitudUsuarioId { get; set; }

}
