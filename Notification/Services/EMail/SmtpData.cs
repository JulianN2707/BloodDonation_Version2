using System.Net.Mail;

namespace MVCT.Sisfv.Tansversales.Notificaciones.Worker.Services.EMail
{
    public class SmtpData
    {
        public string Address { get; set; }
        public SmtpDeliveryFormat DeliveryFormat { get; set; }
        public SmtpDeliveryMethod DeliveryMethod { get; set; }
        public bool EnableSsl { get; set; }
        public string Password { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public bool UseDefaultCredentials { get; set; }
    }
}
