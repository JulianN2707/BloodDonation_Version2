namespace Solicitudes.Domain.Entities
{
    public class EstadoSolicitudUsuario
    {
       public Guid EstadoSolicitudUsuarioId { get; set; }
       public required string Descripcion { get; set; }
       public virtual ICollection<SolicitudUsuario> SolicitudesUsuario { get; set; } = [];
    }
}
