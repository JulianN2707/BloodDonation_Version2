namespace DonacionSangre.Domain.Entities.Test
{
    public class Reserva
    {
        public Guid Id { get; private set; }
        public Guid DonanteId { get; private set; }
        public DateTime FechaReserva { get; private set; }
        public string Estado { get; private set; }

        public Reserva(Guid donanteId, DateTime fechaReserva)
        {
            Id = Guid.NewGuid();
            DonanteId = donanteId;
            FechaReserva = fechaReserva;
            Estado = "Pendiente";
        }

        public void Confirmar() => Estado = "Confirmada";
    }
}
