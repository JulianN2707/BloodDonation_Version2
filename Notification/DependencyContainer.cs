using Microsoft.Extensions.Options;
using MVCT.Sisfv.Tansversales.Notificaciones.Worker.Infrastructure.Configurations;
using MVCT.Sisfv.Tansversales.Notificaciones.Worker.Services.EMail;
using Quartz;
using Quartz.Impl;

namespace MVCT.Sisfv.Tansversales.Notificaciones.Worker
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHostedService<NotificationHostedService>();

            services.Configure<TaskSheduleConfiguration>(configuration.GetSection("QuartzConfiguration"));
            services.Configure<MailEngineConfiguration>(configuration.GetSection("MailEngine"));
            services.Configure<SmtpConfiguration>(configuration.GetSection("SMTP"));
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddSingleton(provider =>
            {
                var quartzConfiguration = provider.GetRequiredService<IOptions<TaskSheduleConfiguration>>().Value;
                return new StdSchedulerFactory().GetScheduler().Result;
            });

            services.AddHostedService<QuartzHostedService>();
            return services;
        }
    }
}
