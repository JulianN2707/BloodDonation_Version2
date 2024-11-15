namespace Solicitudes.Domain.ValueObjects
{
    public class TipoSangre
    {
        public string Grupo { get; }
        public string FactorRH { get; }

        private TipoSangre(string grupo, string factorRH)
        {
            Grupo = grupo;
            FactorRH = factorRH;
        }

        public static TipoSangre Crear(string grupo, string factorRH)
        {
            Validar(grupo, factorRH);
            return new TipoSangre(grupo, factorRH);
        }

        private static void Validar(string grupo, string factorRH)
        {
            var gruposValidos = new HashSet<string> { "A", "B", "AB", "O" };
            var factoresValidos = new HashSet<string> { "+", "-" };

            if (!gruposValidos.Contains(grupo.ToUpper()))
                throw new ArgumentException("Grupo sanguíneo inválido.");

            if (!factoresValidos.Contains(factorRH))
                throw new ArgumentException("Factor RH inválido.");
        }

        public static TipoSangre CrearDesdeCadena(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor) || valor.Length < 2)
                throw new ArgumentException("Valor inválido para TipoSangre.");

            var grupo = valor.Substring(0, valor.Length - 1).ToUpper();  // Extrae el grupo ("A", "B", "O", etc.)
            var factorRH = valor.Substring(valor.Length - 1);  // Extrae el factor ("+" o "-")

            return Crear(grupo, factorRH);
        }

        public override bool Equals(object obj)
        {
            if (obj is TipoSangre other)
            {
                return Grupo == other.Grupo && FactorRH == other.FactorRH;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Grupo, FactorRH);
        }

        public override string ToString()
        {
            return $"{Grupo}{FactorRH}";
        }
    }
}
