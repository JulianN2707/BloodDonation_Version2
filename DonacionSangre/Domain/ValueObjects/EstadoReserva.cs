namespace DonacionSangre.Domain.ValueObjects
{
    public class EstadoReserva
    {
        public string Valor { get; private set; }

        public EstadoReserva(string valor)
        {
            Valor = valor;
        }

        public static EstadoReserva Confirmada()
        {
            return new EstadoReserva("Confirmada");
        }

        public static EstadoReserva Cancelada()
        {
            return new EstadoReserva("Cancelada");
        }

        // Método override para asegurar la igualdad de objetos
        public override bool Equals(object? obj)
        {
            if (obj is not EstadoReserva other) return false;
            return Valor == other.Valor;
        }

        public override int GetHashCode()
        {
            return Valor.GetHashCode();
        }

        public override string ToString()
        {
            return Valor;
        }
        public static EstadoReserva CrearDesdeCadena(string valor)
        {
            return new EstadoReserva(valor);
        }
    }
}
