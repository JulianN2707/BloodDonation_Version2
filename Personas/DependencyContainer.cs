using System.Reflection;
using Carter;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Personas.Infrastructure.Context;
using Personas.Infrastructure.Repositories.PersonaSpecification;
using Personas.Infrastructure.Repositories.SpecificationUnitOfWork;

namespace Personas;

public static class DependencyContainer
{ 
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration){

        services.AddDbContext<PersonasContext>(options =>
            options.UseSqlServer(configuration.GetValue<string>("ConnectionStrings:ConnectionString")));
        services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped
            (typeof(IPersonaSpecificationUnitOfWork), typeof(PersonaSpecificationUnitOfWork));
        services.AddSwaggerGen();
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddCarter();
        return services;
    }

}
