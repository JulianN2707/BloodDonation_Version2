using Microsoft.Extensions.Options;
using MVCT.Sisfv.Tansversales.Notificaciones.Worker.Application.Features.Donacion;
using MVCT.Sisfv.Tansversales.Notificaciones.Worker.Infrastructure.Configurations;
using Quartz;

namespace MVCT.Sisfv.Tansversales.Notificaciones.Worker;

public class NotificationHostedService : IHostedService
{
    private readonly IScheduler _scheduler;
    private readonly TaskSheduleConfiguration _taskScheduleConfiguration;

    public NotificationHostedService(IScheduler scheduler, IOptions<TaskSheduleConfiguration> taskScheduleConfiguration)
    {
        _scheduler = scheduler;
        _taskScheduleConfiguration = taskScheduleConfiguration.Value;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        foreach (var jobConfig in _taskScheduleConfiguration.Jobs)
        {
            if (jobConfig.Enabled)
            {
                ScheduleJob(jobConfig);
            }
        }

        return _scheduler.Start();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return _scheduler.Shutdown();
    }

    private async void ScheduleJob(JobConfiguration jobConfig)
    {
        var jobDetail = CreateJobDetail(jobConfig);
        var cronTrigger = CreateCronTrigger(jobConfig);

        await _scheduler.ScheduleJob(jobDetail, cronTrigger);
    }

    private IJobDetail CreateJobDetail(JobConfiguration jobConfig)
    {
        var jobType = jobConfig.Name switch
        {
            "SolicitarDonacion" => typeof(SolicitarDonacionTaskJob),
            _ => null
        };

        if (jobType == null)
        {
            throw new InvalidOperationException($"Unknown job type: {jobConfig.Name}");
        }

        return JobBuilder.Create(jobType)
            .WithIdentity(jobConfig.Name)
            .Build();
    }

    private ITrigger CreateCronTrigger(JobConfiguration jobConfig)
    {
        return TriggerBuilder.Create()
            .WithIdentity($"{jobConfig.Name}Trigger")
            .WithCronSchedule(jobConfig.CronExpression)
            .Build();
    }
}
