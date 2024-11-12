using System;
using System.Reflection;
using Carter;
using MediatR;
using Usuarios.Infrastructure.Context;
using Usuarios.Infrastructure.Repositories.SpecificationUnitOfWork;

namespace Usuarios;

public static class DependencyContainer
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration){

       services.AddDbContext<UsuariosContext>();

        services.AddScoped
            (typeof(IUsuariosSpecificationUnitOfWork), typeof(UsuariosSpecificationUnitOfWork));

        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddCarter();
        return services;


    }

}
