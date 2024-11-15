using Carter;
using DotNetEnv;
using MassTransit;
using MassTransitMessages.Formatter;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Personas;
using Personas.Application.Consumers;
using Personas.Domain.Common;

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
var appName = configuration.GetValue<string>("AppName") ?? "Persona";

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
        x.AddConsumer<CrearPersonaConsumer>();

        x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(prefix: $"{environment}", includeNamespace: false));

        x.UsingActiveMq((context, cfg) =>
        {
            cfg.Host(configuration.GetValue<string>("MQ_Active_Host")!, configuration.GetValue<int>("MQ_Active_Port"), host =>
            {
                host.Username(configuration.GetValue<string>("MQ_Active_Username")!);
                host.Password(configuration.GetValue<string>("MQ_Active_Password")!);
            });
            
            cfg.MessageTopology.SetEntityNameFormatter(new MessageNameFormatter());

            cfg.ReceiveEndpoint($"crear-persona", x =>
            {
                x.ConfigureConsumeTopology = false;
                x.ConfigureConsumer<CrearPersonaConsumer>(context, cfg =>
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
app.UseSwagger();
app.UseSwaggerUI(setupAction =>
{
    setupAction.DocumentTitle = "PERSONAS API";
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









