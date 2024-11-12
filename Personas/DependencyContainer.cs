using System.Reflection;
using Carter;
using MediatR;
using Personas.Infrastructure.Context;
using Personas.Infrastructure.Repositories.SpecificationUnitOfWork;

namespace Personas;

public static class DependencyContainer
{ 
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration){

       services.AddDbContext<PersonasContext>();

        services.AddScoped
            (typeof(IPersonaSpecificationUnitOfWork), typeof(PersonaSpecificationUnitOfWork));

        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddCarter();
        return services;


    }

}
