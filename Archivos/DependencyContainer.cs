using System;
using System.Reflection;
using Archivos.Infrastructure.Context;
using Archivos.Infrastructure.Repositories.ArchivosSpecification;
using Archivos.Infrastructure.Repositories.SpecificationUnitOfWork;
using Carter;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Archivos;

public static class DependencyContainer
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped
            (typeof(IArchivosSpecificationUnitOfWork), typeof(ArchivosSpecificationUnitOfWork));

        services.AddDbContext<ArchivosContext>(options =>
        options.UseSqlServer(configuration.GetValue<string>("ConnectionStrings:ConnectionString")));

        services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        services.AddSwaggerGen();
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddCarter();
        return services;
    }

}
