using MVCT.Sisfv.Tansversales.Notificaciones.Worker;
using MVCT.Sisfv.Tansversales.Notificaciones.Worker.Application.Features.Donacion;
using MVCT.Sisfv.Tansversales.Notificaciones.Worker.Helpers;
using MVCT.Sisfv.Tansversales.Notificaciones.Worker.Infrastructure.Configurations;
using Serilog;
using Spectre.Console;

static void DisplayJobsInTable(List<JobConfiguration> jobsArray)
{

    var table = new Table();
    table.AddColumn("Name");
    table.AddColumn("CronExpression");
    table.AddColumn("Enabled");

    foreach (var job in jobsArray)
    {
        table.AddRow(job.Name, StringHumanize.HumanizeCronSummary(job.CronExpression), job.Enabled.ToString());
    }

    AnsiConsole.Write(table);
}

static void DisplayTemplatesInTable(MailEngineConfiguration mailConfig)
{
    var table = new Table();

    table.AddColumn("TaskJobName");
    table.AddColumn("Endpoint");
    table.AddColumn("Subject");
    table.AddColumn("TemplateHtml");

    if (mailConfig?.Templates?.Count > 0)
    {
        foreach (var template in mailConfig.Templates)
        {
            table.AddRow(
                template.TaskJobName,
                template.Endpoint,
                template.Subject,
                template.TemplateHtml
            );
        }
    }
    else
    {
        table.AddRow("No templates found.");
    }

    AnsiConsole.Write(table);
}

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

var configuration = new ConfigurationBuilder().
    AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
    .Build();

Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
var quartzConfigSection = configuration.GetSection("QuartzConfiguration");

if (quartzConfigSection == null)
{
    Console.WriteLine("QuartzConfiguration section not found in appsettings.json");
    return;
}

var jobsArray = quartzConfigSection.GetSection("Jobs")?.Get<List<JobConfiguration>>();

if (jobsArray == null)
{
    Console.WriteLine("Jobs array not found in QuartzConfiguration section");
    return;
}


DisplayJobsInTable(jobsArray);
IConfigurationSection mailEngineSection = configuration.GetSection("MailEngine");

var mailConfig = mailEngineSection.Get<MailEngineConfiguration>()!;
DisplayTemplatesInTable(mailConfig);

SolicitarDonacionTaskJob.Initialize(configuration);

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddServices(configuration);
var host = builder.Build();
host.Run();
