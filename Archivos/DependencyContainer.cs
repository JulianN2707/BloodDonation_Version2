using System;
using System.Reflection;
using Archivos.Infrastructure.Context;
using Archivos.Infrastructure.Repositories.SpecificationUnitOfWork;
using Carter;
using MediatR;

namespace Archivos;

public static class DependencyContainer
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration){

       services.AddDbContext<ArchivosContext>();

        services.AddScoped
            (typeof(IArchivosSpecificationUnitOfWork), typeof(ArchivosSpecificationUnitOfWork));

        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddCarter();
        return services;


    }

}
