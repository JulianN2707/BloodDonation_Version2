using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Spectre.Console;

namespace MVCT.Transversales.Services.Notificaciones.Services;

/// <summary>
/// 
/// </summary>
public class JaegerExporterHostedService : IHostedService
{
    private readonly IConfiguration _configuration;
    private readonly string _appName;
    private readonly string _environment;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configuration"></param>
    public JaegerExporterHostedService(IConfiguration configuration)
    {
        _configuration = configuration;
        _appName = _configuration.GetValue<string>("AppName") ?? "Notificaciones";
        _environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Unknown";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task StartAsync(CancellationToken cancellationToken)
    {
        var agentHost = _configuration.GetValue<string>("Tracing:AgentHost");
        var agentPort = _configuration.GetValue<int>("Tracing:AgentPort");

        try
        {
            // Manual Jaeger setup
            var tracerProvider = Sdk.CreateTracerProviderBuilder()
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService($"{_appName.ToLower()}-api-{_environment}"))
                .AddJaegerExporter(opts =>
                {
                    opts.AgentHost = agentHost;
                    opts.AgentPort = agentPort;
                })
                .Build();

            AnsiConsole.MarkupLine($"Jaeger Agent Host: [bold yellow]{agentHost}[/]");
            AnsiConsole.MarkupLine($"Jaeger Agent Port: [bold yellow]{agentPort}[/]");
            AnsiConsole.MarkupLine("[bold green]Jaeger exporter added successfully.[/]");
        }
        catch (System.Net.Sockets.SocketException ex)
        {
            AnsiConsole.MarkupLine($"[bold red]Failed to connect to Jaeger at {agentHost}:{agentPort}. Continuing without Jaeger.[/]");
            AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[bold red]Unexpected error while configuring Jaeger: {ex.Message}[/]");
            AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}