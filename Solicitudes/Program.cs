using Carter;
using DotNetEnv;
using MassTransit;
using MassTransitMessages.Formatter;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Solicitudes;
using Solicitudes.Application.Consumers;
using Solicitudes.Application.Sagas.Saga;
using Solicitudes.Application.Sagas.StateInstances;
using Solicitudes.Domain.Common;

var builder = WebApplication.CreateBuilder(args);
Console.Clear();

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
builder.Configuration.Sources.Clear();
if (environment != "staging") Env.Load();

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
var configuration = builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();
var appName = configuration.GetValue<string>("AppName") ?? "Solicitudes";

builder.Services.Configure<AppSettings>(configuration.GetSection(AppSettings.SectionKey));

if (environment != "staging")
{
    var kestrelPort = configuration.GetValue<int>("HttpKestrelPort");
    ConfigureKestrel(builder, kestrelPort);

}

var databaseConnection = configuration.GetValue<string>("ConnectionStrings:ConnectionString");
// Add services to the container.
builder.Services.AddInfrastructureServices(configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

#region Healthcheck
builder.Services.AddHealthChecks();
#endregion

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()
               .WithExposedHeaders("X-Pagination");
    });
});
#region configuration MQ
var providerMQ = configuration.GetValue<string>("MQ_Provider");

if (providerMQ.Equals("activeMQ"))
{
    #region active config-consumer
    builder.Services.AddMassTransit(x =>
    {
        x.AddConsumer<ObtenerInformacionSolicitudConsumer>();
        x.AddConsumer<RollBackEliminarSolicitudConsumer>();
        x.AddConsumer<RollBackDesaprobarSolicitudConsumer>();
        x.AddConsumer<EnviarNotificacionConsumer>();
        x.AddSagaStateMachine<SolicitudDonanteCreadoStateMachine, SolicitudCrearDonanteStateInstance>().InMemoryRepository();
        x.AddSagaStateMachine<SolicitudAprobarDonanteStateMachine, SolicitudAprobarDonanteStateInstance>().InMemoryRepository();

        x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(prefix: $"{environment}", includeNamespace: false));

        x.UsingActiveMq((activeContext, activeConfig) =>
        {
            activeConfig.Host(configuration.GetValue<string>("MQ_Active_Host")!, configuration.GetValue<int>("MQ_Active_Port"), h =>
            {
                h.Username(configuration.GetValue<string>("MQ_Active_Username")!);
                h.Password(configuration.GetValue<string>("MQ_Active_Password")!);
            });

            activeConfig.MessageTopology.SetEntityNameFormatter(new MessageNameFormatter());

            activeConfig.ReceiveEndpoint($"saga-aprobar-donante", e =>
            {
                e.ConfigureConsumeTopology = false;
                e.UseMessageRetry(r => r.Interval(5,TimeSpan.FromMinutes(4)));
                e.UseDelayedRedelivery(r => r.Intervals(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(30)));
                e.ConfigureSaga<SolicitudCrearDonanteStateInstance>(activeContext);           
                
            });
            activeConfig.ReceiveEndpoint($"saga-crear-donante", e =>
            {
                e.ConfigureConsumeTopology = false;
                e.UseMessageRetry(r => r.Interval(5,TimeSpan.FromMinutes(4)));
                e.UseDelayedRedelivery(r => r.Intervals(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(30)));
                e.ConfigureSaga<SolicitudAprobarDonanteStateInstance>(activeContext);
            });

            activeConfig.ReceiveEndpoint($"obtener-informacion-solicitud", x =>
            {
                x.ConfigureConsumeTopology = false;
                x.ConfigureConsumer<ObtenerInformacionSolicitudConsumer>(activeContext, cfg =>
                {
                    cfg.UseDelayedRedelivery(r => r.Intervals(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(30)));
                    cfg.UseMessageRetry(r => r.Interval(5,TimeSpan.FromMinutes(4)));
                    cfg.UseInMemoryOutbox(activeContext);

                });
            });

            activeConfig.ReceiveEndpoint($"eliminar-solicitud", x =>
            {
                x.ConfigureConsumeTopology = false;
                x.ConfigureConsumer<RollBackEliminarSolicitudConsumer>(activeContext, cfg =>
                {
                    cfg.UseDelayedRedelivery(r => r.Intervals(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(30)));
                    cfg.UseMessageRetry(r => r.Interval(5,TimeSpan.FromMinutes(4)));
                    cfg.UseInMemoryOutbox(activeContext);
                });
            });
            activeConfig.ReceiveEndpoint($"desaprobar-solicitud", x =>
            {
                x.ConfigureConsumeTopology = false;
                x.ConfigureConsumer<RollBackDesaprobarSolicitudConsumer>(activeContext, cfg =>
                {
                    cfg.UseDelayedRedelivery(r => r.Intervals(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(30)));
                    cfg.UseMessageRetry(r => r.Interval(5,TimeSpan.FromMinutes(4)));
                    cfg.UseInMemoryOutbox(activeContext);
                });
            });
            activeConfig.ReceiveEndpoint($"enviar-notificacion", x =>
            {
                x.ConfigureConsumeTopology = false;
                x.ConfigureConsumer<EnviarNotificacionConsumer>(activeContext, cfg =>
                {
                    cfg.UseDelayedRedelivery(r => r.Intervals(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(30)));
                    cfg.UseMessageRetry(r => r.Interval(5,TimeSpan.FromMinutes(4)));
                    cfg.UseInMemoryOutbox(activeContext);
                });
            });
            activeConfig.EnableArtemisCompatibility();
        });
    });
    #endregion
}
#endregion

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(setupAction =>
{
    setupAction.DocumentTitle = "SolicitudesAPI";
    setupAction.DefaultModelsExpandDepth(-1);
    setupAction.DisplayOperationId();
    setupAction.DisplayRequestDuration();
});

app.UseHealthChecks("/healthz");
app.UseRouting();
app.MapCarter();
app.UseCors("AllowAnyOrigin");
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

