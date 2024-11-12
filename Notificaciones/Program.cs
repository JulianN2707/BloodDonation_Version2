
using DotNetEnv;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.OpenApi.Models;
using MVCT.Transversales.Services.Notificaciones;
using Spectre.Console;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Runtime.InteropServices;
using MVCT.Transversales.Services.Notificaciones.Services;

var builder = WebApplication.CreateBuilder(args);

Console.Clear();
var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Unknown";
builder.Configuration.Sources.Clear();
DisplaySystemInfo();
DisplayEnvironmentMessage(environment);
if(environment!="staging")  Env.Load();
var configuration = builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables().Build();
    var appName = configuration.GetValue<string>("AppName") ?? "Notificaciones";
//-------------------------------------------------------------------------------------------
if (environment != "staging")
{
    var kestrelPort = configuration.GetValue<int>("HttpKestrelPort");
    ConfigureKestrel(builder, kestrelPort);
   
}
var useInstrumentation = configuration.GetValue<bool>("UseInstrumentation");
if (useInstrumentation)
{
    var agentHost = configuration.GetValue<string>("Tracing:AgentHost");
    var agentPort = configuration.GetValue<int>("Tracing:AgentPort");
    ConfigureOpenTelemetry(builder, appName, environment );
}


DisplayAppName(appName);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc
    (
        "openAPISpecification",
        new OpenApiInfo
        {
            Title = "Transversales Notificaciones API",
            Description = "Api para envio de notificaciones.",
            TermsOfService = new Uri("https://www.minvivienda.gov.co"),
            Contact = new OpenApiContact
            {
                Name = "UT NexPass",
                Url = new Uri("https://www.nexura.com"),
                Email = "soporte@nexura.com"
            },
            Version = "1.0"
        });
});


builder.Services.AddNotificacionServices();
builder.Services.AddCors(options =>
    options.AddDefaultPolicy(config =>
        config
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin()));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint(
        "/swagger/openAPISpecification/swagger.json", "Transversales Notificaciones API");

    c.DocumentTitle = "MVCT.Transversales.Services.Notificaciones";
    c.DefaultModelsExpandDepth(-1);
    c.DisplayOperationId();
    c.DisplayRequestDuration();
});

app.UseCors();
app.UseNotificacionesEndpoints();
DisplayEnvironmentVariablesTable();
app.Run();

void DisplayEnvironmentMessage(string currentEnvironment)
{
    AnsiConsole.MarkupLine($"Running for: [bold yellow]{currentEnvironment}[/] environment");
}

void ConfigureKestrel(WebApplicationBuilder contextBuilder, int kestrelPort)
{
    contextBuilder.WebHost.ConfigureKestrel(options =>
    {
        options.ListenAnyIP(kestrelPort, listenOptions =>
        {
            listenOptions.Protocols = HttpProtocols.Http2 | HttpProtocols.Http1;
        });
    });
    AnsiConsole.MarkupLine($"Using Kestrel On Port: [bold yellow]{kestrelPort}[/]");
}

void DisplayAppName(string appName)
{
    var figletText = new FigletText(appName).Centered().Color(Color.Aqua);
    AnsiConsole.Write(figletText);
}
void DisplaySystemInfo()
{
    var os = RuntimeInformation.OSDescription;
    var timeZone = TimeZoneInfo.Local;
    var currentTime = DateTime.Now;
    
    var table = new Table();
    table.AddColumn("System Info");
    table.AddColumn("Details");

    table.AddRow("Operating System", os);
    table.AddRow("Time Zone", timeZone.DisplayName);
    table.AddRow("Current Date Time", currentTime.ToString("yyyy-MM-dd HH:mm:ss"));

    AnsiConsole.Write(table);
}


void ConfigureOpenTelemetry(WebApplicationBuilder builder, string appName, string environment)
{
    builder.Services.AddOpenTelemetry().WithTracing(tracerProviderBuilder =>
    {
        tracerProviderBuilder
            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService($"{appName.ToLower()}-api-{environment}"))
            .AddAspNetCoreInstrumentation();
    });

    builder.Host.UseServiceProviderFactory(new DefaultServiceProviderFactory());
    
    builder.Host.ConfigureServices((context, services) =>
    {
        services.AddSingleton<IHostedService, JaegerExporterHostedService>();
    });
}


void DisplayEnvironmentVariablesTable()
{
    var table = new Table();
    table.AddColumn("Key");
    table.AddColumn("Value");

    var environmentVariables = Environment.GetEnvironmentVariables();
    foreach (var key in environmentVariables.Keys)
    {
        table.AddRow(key.ToString(), environmentVariables[key]?.ToString() ?? "null");
    }

    AnsiConsole.Write(table);
}
