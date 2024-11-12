using MVCT.Transversales.Services.Notificaciones.Application.Features.Notificacion;



namespace MVCT.Transversales.Services.Notificaciones;

public static class NotificacionesEndpoints
{
    public static WebApplication UseNotificacionesEndpoints(this WebApplication app)
    {
        app.UseNotificacionEndpoint();
        return app;
    }
}