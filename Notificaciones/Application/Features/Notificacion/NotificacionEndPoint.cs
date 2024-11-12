using Microsoft.AspNetCore.Mvc;
using MVCT.Transversales.Services.Notificaciones.Domain.Dtos;
using MVCT.Transversales.Services.Notificaciones.Services;

namespace MVCT.Transversales.Services.Notificaciones.Application.Features.Notificacion
{
    /// <summary>
    /// 
    /// </summary>
    public static class NotificacionEndPoint
    {
        private static IEMailService _emailService;

        // Static method to initialize the email service instance
        public static void Initialize(IEMailService emailService)
        {
            _emailService = emailService;
        }

        public static WebApplication UseNotificacionEndpoint(this WebApplication app)
        {
            if (_emailService == null)
            {
                throw new InvalidOperationException("Email Service is not initialized. Call Initialize method first.");
            }


            _ = app.MapPost("/api/v1/notificacion", async Task<SendEMailResponse> ([FromBody] SendEMailRequest request) =>
            {

                return await _emailService.SendEMail(request);

            }).WithTags("Notificaciones").WithOpenApi();
            return app;
        }
    }
}
