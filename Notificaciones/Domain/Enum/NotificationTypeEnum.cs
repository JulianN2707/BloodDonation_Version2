using System.ComponentModel;

namespace MVCT.Transversales.Services.Notificaciones.Domain.Enum
{
    public enum NotificationType
    {
        [Description("EMAIL")]
        EMAIL, 
        [Description("SMS")]
        SMS,
        [Description("OTP")]
        OTP
    }
}
