using DonacionSangre.Domain.Entities;
using Microsoft.Identity.Client;

namespace DonacionSangre.Application.Common
{
    public class Tags
    {
        public static string RutaBase = "api/";

        public class ReservaDonacion
        {
            public static string Tag = "Reserva donacion";
        }

        public class SolicitudDonacion
        {
            public static string Tag = "Solicitud donacion";
        }
    }
}
