namespace Archivos.Domain.Entities
{
    public class TipoArchivo
    {
        public Guid TipoArchivoId { get; set; }
        public required string Descripcion {  get; set; }
        public virtual ICollection<Archivo> Archivos { get; set; } = [];
    }
}
