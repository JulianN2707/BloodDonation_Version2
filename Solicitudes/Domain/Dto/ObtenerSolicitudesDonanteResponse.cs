using Solicitudes.Domain.Entities;
using Solicitudes.Domain.ValueObjects;

namespace Solicitudes.Domain.Dto
{
    public class ObtenerSolicitudesDonanteResponse
    {
        public Guid SolicitudUsuarioId { get; set; }
        public Guid? PersonaMunicipioDireccionId { get; set; }
        public Guid TipoPersonaId { get; set; }
        public Guid EstadoSolicitudUsuarioId { get; set; }
        public required string TipoSangre { get; set; }
        public string? PersonaNumeroDocumento { get; set; }
        public DateTime? PersonaFechaExpedicionDocumento { get; set; }
        public string? PersonaPrimerApellido { get; set; }
        public string? PersonaPrimerNombre { get; set; }
        public string? PersonaSegundoApellido { get; set; }
        public string? PersonaSegundoNombre { get; set; }
        public string? PersonaCorreoElectronico { get; set; }
        public string? PersonaCelular { get; set; }
        public string? PersonaDireccion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public string? MotivoRechazo { get; set; }
        public DateTime? FechaRechazo { get; set; }
        public string EstadoSolicitudUsuario { get; set; }
    }
}
