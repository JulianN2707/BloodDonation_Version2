using System;

namespace Solicitudes.Domain.Entities;

public class SolicitudUsuario
{
    public Guid SolicitudUsuarioId { get; set; }
    public Guid? PersonaMunicipioDireccionId { get; set; }
    public string? PersonaNumeroDocumento { get; set; }
    public DateTime? PersonaFechaExpedicionDocumento { get; set; }
    public string? PersonaPrimerApellido { get; set; }
    public string? PersonaPrimerNombre { get; set; }
    public string? PersonaSegundoApellido { get; set; }
    public string? PersonaSegundoNombre { get; set; }
    public string? PersonaCorreoElectronico { get; set; }
    public string? PersonaCelular { get; set; }
    public string? PersonaDireccion { get; set; }
    public int EstadoSolicitudUsuarioId { get; set; }
    public Guid TipoPersonaId { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime? FechaAprobacion { get; set; }
    public string? MotivoRechazo { get; set; }
    public DateTime? FechaRechazo { get; set; }

    public SolicitudUsuario CrearSolicitud(
    string? personaNumeroDocumento, 
    DateTime? personaFechaExpedicionDocumento,
    string? personaPrimerApellido, 
    string? personaPrimerNombre, 
    string? personaSegundoApellido, 
    string? personaSegundoNombre,
    string? personaCorreoElectronico, 
    int estadoSolicitudUsuarioId, 
    Guid tipoPersonaId, 
    string? personaCelular = null, 
    string? personaDireccion = null, 
    Guid? personaMunicipioDireccionId = null)
{
    return new SolicitudUsuario
    {
        PersonaNumeroDocumento = personaNumeroDocumento,
        PersonaFechaExpedicionDocumento = personaFechaExpedicionDocumento,
        PersonaPrimerApellido = personaPrimerApellido,
        PersonaPrimerNombre = personaPrimerNombre,
        PersonaSegundoApellido = personaSegundoApellido,
        PersonaSegundoNombre = personaSegundoNombre,
        PersonaCorreoElectronico = personaCorreoElectronico,
        EstadoSolicitudUsuarioId = estadoSolicitudUsuarioId,
        TipoPersonaId = tipoPersonaId,
        FechaCreacion = DateTime.Now,
        PersonaCelular = personaCelular,
        PersonaDireccion = personaDireccion,
        PersonaMunicipioDireccionId = personaMunicipioDireccionId
    };
}

}
