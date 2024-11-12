namespace MVCT.Sisfv.Tansversales.Notificaciones.Worker.Infrastructure.Configurations
{
    public class SmtpConfiguration
    {
        public string Server { get; set; }
        public bool DefaultCredentials { get; set; }
        public int Port { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool SSL { get; set; }
    }
}
