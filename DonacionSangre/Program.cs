using Carter;
using DonacionSangre.Application.Consumers;
using DonacionSangre.Domain.Interfaces;
using DonacionSangre.Domain.Interfaces.MongoRepository;
using DonacionSangre.Domain.Interfaces.SqlServerRepository;
using DonacionSangre.Domain.Services;
using DonacionSangre.Infrastructure;
using DonacionSangre.Infrastructure.MongoRepositories.ReservaDonacionMongoRepository;
using DonacionSangre.Infrastructure.MongoRepositories.SolicitudDonacionMongoRepository;
using DonacionSangre.Infrastructure.Repositories.MongoDbRepository;
using DonacionSangre.Infrastructure.Services.NotificacionesAutomaticas;
using DonacionSangre.Infrastructure.Services.Sincronizacion;
using DonacionSangre.Infrastructure.SqlServerRepositories.Donante;
using DonacionSangre.Infrastructure.SqlServerRepositories.Repository;
using DonacionSangre.Infrastructure.SqlServerRepositories.Reserva;
using DotNetEnv;
using MassTransit;
using MassTransitMessages.Formatter;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
builder.Configuration.Sources.Clear();
if (environment != "staging") Env.Load();

var configuration = builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();
var appName = configuration.GetValue<string>("AppName") ?? "blood";

if (environment != "staging")
{
    var kestrelPort = configuration.GetValue<int>("HttpKestrelPort");
    ConfigureKestrel(builder, kestrelPort);

}

// Configuraci�n de servicios
// Agregar SQL Server DbContext
builder.Services.AddDbContext<SqlDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection")));

// Configuraci�n de los Repositorios
builder.Services.AddScoped<IDonanteRepository, DonanteRepository>();
builder.Services.AddScoped<IReservaRepository, ReservaRepository>();
builder.Services.AddScoped<INotificacionAutomaticaService, NotificacionAutomaticaService>();

//CORS
builder.Services.AddCors(options =>
options.AddDefaultPolicy(config =>
config
.AllowAnyHeader()
.AllowAnyMethod()
.AllowAnyOrigin()));

// Configuraci�n del Repositorio de MongoDB
builder.Services.AddSingleton<IReservaDonacionMongoRepository, ReservaDonacionMongoRepository>();
builder.Services.AddSingleton<IDonanteMongoRepository, DonanteMongoRepository>();
builder.Services.AddSingleton<ISolicitudDonacionMongoRepository, SolicitudDonacionMongoRepository>();
builder.Services.AddSingleton<IPersonaMongoRepository, PersonaMongoRepository>();
builder.Services.AddSingleton<ICentroSaludMongoRepository, CentroSaludMongoRepository>();
builder.Services.AddSingleton<IReservaDonacionService, ReservaDonacionService>();

// Servicio de Sincronizaci�n
builder.Services.AddScoped<SynchronizationService>();
builder.Services.AddCarter();

// Agregar MediatR para CQRS
builder.Services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(Program).Assembly));

// Agregar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Agregar controladores y otros servicios MVC si es necesario
builder.Services.AddControllers();

//Repositorio SQL server generico 
builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

#region configuration MQ
var providerMQ = configuration.GetValue<string>("MQ_Provider");

if (providerMQ.Equals("activeMQ"))
{
    #region active config-consumer
    builder.Services.AddMassTransit(x =>
    {
        x.AddConsumer<CrearDonanteConsumer>();

        x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(prefix: $"{environment}", includeNamespace: false));

        x.UsingActiveMq((context, cfg) =>
        {
            cfg.Host(configuration.GetValue<string>("MQ_Active_Host")!, configuration.GetValue<int>("MQ_Active_Port"), host =>
            {
                host.Username(configuration.GetValue<string>("MQ_Active_Username")!);
                host.Password(configuration.GetValue<string>("MQ_Active_Password")!);
            });
            
            cfg.MessageTopology.SetEntityNameFormatter(new MessageNameFormatter());

            cfg.ReceiveEndpoint($"crear-archivos", x =>
            {
                x.ConfigureConsumeTopology = false;
                x.ConfigureConsumer<CrearDonanteConsumer>(context, cfg =>
                {
                    cfg.UseDelayedRedelivery(r => r.Intervals(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(30)));
                    cfg.UseMessageRetry(r => r.Interval(5,TimeSpan.FromMinutes(4)));
                    cfg.UseInMemoryOutbox(context);
                });
            });
            cfg.EnableArtemisCompatibility();

        });
    });
    #endregion
}
#endregion

var app = builder.Build();

// Configurar middleware de la aplicaci�n
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();  // Habilita Swagger para generar los documentos
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapCarter();
app.MapControllers();

app.Run();


void ConfigureKestrel(WebApplicationBuilder contextBuilder, int kestrelPort)
{
    contextBuilder.WebHost.ConfigureKestrel(options =>
    {
        options.ListenAnyIP(kestrelPort, listenOptions =>
        {
            listenOptions.Protocols = HttpProtocols.Http2 | HttpProtocols.Http1;
        });
    });
}
