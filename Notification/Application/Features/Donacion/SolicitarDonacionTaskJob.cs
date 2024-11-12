using MVCT.Sisfv.Tansversales.Notificaciones.Worker.Infrastructure.Configurations;
using MVCT.Sisfv.Tansversales.Notificaciones.Worker.Services.Donacion;
using MVCT.Sisfv.Tansversales.Notificaciones.Worker.Services.EMail;
using Quartz;
using Serilog;
using Spectre.Console;

namespace MVCT.Sisfv.Tansversales.Notificaciones.Worker.Application.Features.Donacion;

internal class SolicitarDonacionTaskJob : IJob
{
    private static IConfiguration _configuration;

    public static void Initialize(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            await AnsiConsole.Status()
                .AutoRefresh(false)
                .Spinner(Spinner.Known.Star)
                .SpinnerStyle(Style.Parse("green bold"))
                .StartAsync("SolicitarDonacionTaskJob is running...", async ctx =>
                {
                    var taskJobName = context.JobDetail.JobType.Name;
                    IConfigurationSection mailEngineSection = _configuration.GetSection("MailEngine");

                    var mailConfig = mailEngineSection.Get<MailEngineConfiguration>();
                    var template = mailConfig.Templates.FirstOrDefault(x => x.TaskJobName == taskJobName);
                    Log.Information("SolicitarDonacionTaskJob Templates Config Retrieve...");
                    await Task.Delay(1000); 
                    await Task.Delay(1000); 

                    var client = new DonacionApiClient(template.Endpoint, template);

                    var list = await client.ObtenerSolicitudesDonacion();
                    Log.Information($"SolicitarDonacionTaskJob Retrieved recipients ...{list.Count}");
                    await Task.Delay(1000); 

                    await new EmailService(_configuration).SendBatch(list);
                    Log.Information($"SolicitarDonacionTaskJob Sended Mail to recipients ...{list.Count}");
                    await Task.Delay(1000); 
                });
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred while executing the job");
            throw;
        }
        finally
        {
            AnsiConsole.MarkupLine("SolicitarDonacionTaskJob finished");
            Log.CloseAndFlush();
        }
    }
}
