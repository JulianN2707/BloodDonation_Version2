namespace DonacionSangre.Domain.ValueObjects
{
    public class EstadoSolicitudDonacion
    {
        public string Valor { get; private set; }

        private EstadoSolicitudDonacion(string valor)
        {
            Valor = valor;
        }

        public static EstadoSolicitudDonacion Activo()
        {
            return new EstadoSolicitudDonacion("Activo");
        }

        public static EstadoSolicitudDonacion Inactivo()
        {
            return new EstadoSolicitudDonacion("Inactivo");
        }

        // Método override para asegurar la igualdad de objetos
        public override bool Equals(object? obj)
        {
            if (obj is not EstadoSolicitudDonacion other) return false;
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
        public static EstadoSolicitudDonacion CrearDesdeCadena(string valor)
        {
            return new EstadoSolicitudDonacion(valor);
        }
    }
}
