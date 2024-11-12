using System;
using System.Reflection;
using Carter;
using MediatR;
using Solicitudes.Infrastructure.Context;
using Solicitudes.Infrastructure.Repositories.SpecificationUnitOfWork;
using Solicitudes.Infrastructure.Services.NotificacionesService;

namespace Solicitudes;

public static class DependencyContainer
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration){

       services.AddDbContext<SolicitudesContext>();

        services.AddScoped
            (typeof(ISolicitudesSpecificationUnitOfWork), typeof(SolicitudesSpecificationUnitOfWork));

        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddSingleton<IEmailService, EMailService>();
        services.AddCarter();
        return services;


    }

}
