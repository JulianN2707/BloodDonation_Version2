using System;
using System.Reflection;
using Carter;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Solicitudes.Infrastructure.Context;
using Solicitudes.Infrastructure.Repositories.SolicitudesSpecification;
using Solicitudes.Infrastructure.Repositories.SpecificationUnitOfWork;
using Solicitudes.Infrastructure.Services.NotificacionesService;

namespace Solicitudes;

public static class DependencyContainer
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration){

        services.AddDbContext<SolicitudesContext>(options =>
        options.UseSqlServer(configuration.GetValue<string>("ConnectionStrings:ConnectionString")));
        services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        services.AddSwaggerGen();
        services.AddScoped
            (typeof(ISolicitudesSpecificationUnitOfWork), typeof(SolicitudesSpecificationUnitOfWork));

        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddSingleton<IEmailService, EMailService>();
        services.AddCarter();
        return services;


    }

}
