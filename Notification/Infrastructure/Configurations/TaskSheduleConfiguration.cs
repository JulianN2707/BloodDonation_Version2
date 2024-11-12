namespace MVCT.Sisfv.Tansversales.Notificaciones.Worker.Infrastructure.Configurations
{
    public class TaskSheduleConfiguration
    {
        public List<JobConfiguration> Jobs { get; set; }
    }

    public class JobConfiguration
    {
        public string Name { get; set; }
        public string CronExpression { get; set; }
        public bool Enabled { get; set; }
    }
}
