using System;

namespace Solicitudes.Domain.Entities;

public class SolicitudUsuario
    {
        public Guid SolicitudUsuarioId { get; set; }
        public string? PersonaNumeroDocumento { get; set; }
        public DateTime? PersonaFechaExpedicionDocumento { get; set; }
        public string? PersonaPrimerApellido { get; set; }
        public string? PersonaPrimerNombre { get; set; }
        public string? PersonaSegundoApellido { get; set; }
        public string? PersonaSegundoNombre { get; set; }
        public string? PersonaCorreoElectronico { get; set; }
        public int? PersonaMunicipioDireccionId { get; set; }
        public string? PersonaCelular { get; set; }
        public string? PersonaDireccion { get; set; }
        public int EstadoSolicitudUsuarioId { get; set; }
        public int TipoCargoPersonaId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public string? MotivoRechazo { get; set; }
        public DateTime? FechaRechazo { get; set; }
    }
