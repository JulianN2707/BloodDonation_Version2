namespace Personas.Domain.Dto
{
    public class ObtenerPersonaPorIdResponse
    {
        public Guid PersonaId { get; set; }
        public string NumeroDocumento { get; set; } = null!;
        public DateTime? FechaExpedicionDocumento { get; set; }
        public string? PrimerApellido { get; set; }
        public string? PrimerNombre { get; set; }
        public string? SegundoApellido { get; set; }
        public string? SegundoNombre { get; set; }
        public Guid? MunicipioDireccionId { get; set; }
        public string? CorreoElectronico { get; set; }
        public string? Celular { get; set; }
        public string? Direccion { get; set; }
    }
}
