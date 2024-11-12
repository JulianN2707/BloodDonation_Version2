using MVCT.Transversales.Services.Notificaciones.Application.Features.Notificacion;
using MVCT.Transversales.Services.Notificaciones.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyContainer
{
    public static IServiceCollection AddNotificacionServices(this IServiceCollection services)
    {
        services.AddSingleton<IEMailService, EMailService>();
        NotificacionEndPoint.Initialize(services.BuildServiceProvider().GetService<IEMailService>());

        return services;
    }
}



