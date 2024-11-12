using MVCT.Transversales.Services.Notificaciones.Domain.Dtos;

namespace MVCT.Transversales.Services.Notificaciones.Services
{
    public interface IEMailService
    {
        Task<SendEMailResponse> SendEMail(SendEMailRequest sendEmailRequest);
    }
}