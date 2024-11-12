using MediatR;

namespace DonacionSangre.Application.Features.Donante.Command.ActualizarDonante
{
    public class ActualizarDonanteCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string TipoSangre { get; set; }
        public string ZonaResidencia { get; set; }
        public ActualizarDonanteCommand(Guid id, string nombre, string tipoSangre, string zonaResidencia)
        {
            Id = id;
            Nombre = nombre;
            TipoSangre = tipoSangre;
            ZonaResidencia = zonaResidencia;
        }
    }
}
