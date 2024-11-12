namespace DonacionSangre.Domain.Entities.Test
{
    public class Donante
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string TipoSangre { get; set; }
        public string ZonaResidencia { get; set; }

        public Donante(string nombre, string tipoSangre, string zonaResidencia)
        {
            Id = Guid.NewGuid();
            Nombre = nombre;
            TipoSangre = tipoSangre;
            ZonaResidencia = zonaResidencia;
        }
    }
}
