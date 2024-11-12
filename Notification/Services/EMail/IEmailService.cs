using MVCT.Sisfv.Tansversales.Notificaciones.Worker.Domain.Dto;

namespace MVCT.Sisfv.Tansversales.Notificaciones.Worker.Services.EMail
{
    public interface IEmailService
    {
        Task SendBatch(List<Recipient> recipients);
    }
}
