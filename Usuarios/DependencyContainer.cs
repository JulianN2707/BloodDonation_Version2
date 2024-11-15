using System;
using System.Reflection;
using Carter;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Usuarios.Infrastructure.Context;
using Usuarios.Infrastructure.Repositories.SpecificationUnitOfWork;
using Usuarios.Infrastructure.Repositories.UsuariosSpecification;

namespace Usuarios;

public static class DependencyContainer
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration){

       services.AddDbContext<UsuariosContext>(options =>
        options.UseSqlServer(configuration.GetValue<string>("ConnectionStrings:ConnectionString")));
        services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

        services.AddScoped
            (typeof(IUsuariosSpecificationUnitOfWork), typeof(UsuariosSpecificationUnitOfWork));
        services.AddSwaggerGen();
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddCarter();
        return services;
    }

}
