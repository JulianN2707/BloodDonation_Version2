using System;
using System.Reflection;
using Carter;
using MediatR;
using Solicitudes.Infrastructure.Context;
using Solicitudes.Infrastructure.Repositories.SpecificationUnitOfWork;

namespace Solicitudes;

public static class DependencyContainer
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration){

       services.AddDbContext<SolicitudesContext>();

        services.AddScoped
            (typeof(ISolicitudesSpecificationUnitOfWork), typeof(SolicitudesSpecificationUnitOfWork));

        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddCarter();
        return services;


    }

}
