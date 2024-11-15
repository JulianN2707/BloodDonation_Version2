using MediatR;
using Solicitudes.Domain.Dto;
using Solicitudes.Domain.Entities;
using Solicitudes.Domain.Specification;
using Solicitudes.Infrastructure.Repositories.SolicitudesSpecification;

namespace Solicitudes.Application.Features.SolicitudDonante.Queries.ObtenerSolicitudesDonante
{
    public class ObtenerSolicitudesDonanteQueryHandler : IRequestHandler<ObtenerSolicitudesDonanteQuery, List<ObtenerSolicitudesDonanteResponse>>
    {
        private readonly IRepository<SolicitudUsuario> _solicitudUsuarioRepository;

        public ObtenerSolicitudesDonanteQueryHandler(IRepository<SolicitudUsuario> solicitudUsuarioRepository)
        {
            _solicitudUsuarioRepository = solicitudUsuarioRepository;
        }

        public async Task<List<ObtenerSolicitudesDonanteResponse>> Handle(ObtenerSolicitudesDonanteQuery request, CancellationToken cancellationToken)
        {
            var spec = new ObtenerSolicitudesDonanteSpecification();
            return (await _solicitudUsuarioRepository.ListAsync(spec, cancellationToken)).Select(x => 
            new ObtenerSolicitudesDonanteResponse
            {
                EstadoSolicitudUsuario = x.EstadoSolicitudUsuario.Descripcion,
                TipoSangre = x.TipoSangre.ToString(),
                FechaAprobacion = x.FechaAprobacion,
                FechaCreacion = x.FechaCreacion,
                FechaRechazo = x.FechaRechazo,
                MotivoRechazo = x.MotivoRechazo,
                PersonaCelular = x.PersonaCelular,
                PersonaCorreoElectronico = x.PersonaCorreoElectronico,
                PersonaDireccion = x.PersonaDireccion,
                PersonaFechaExpedicionDocumento = x.PersonaFechaExpedicionDocumento,
                PersonaMunicipioDireccionId = x.PersonaMunicipioDireccionId,
                PersonaNumeroDocumento = x.PersonaNumeroDocumento,
                PersonaPrimerApellido = x.PersonaPrimerApellido,
                PersonaPrimerNombre = x.PersonaPrimerNombre,
                PersonaSegundoApellido = x.PersonaSegundoApellido,
                PersonaSegundoNombre = x.PersonaSegundoNombre,
                TipoPersonaId = x.TipoPersonaId,
                SolicitudUsuarioId = x.SolicitudUsuarioId
            }).ToList();
        }
    }
}
