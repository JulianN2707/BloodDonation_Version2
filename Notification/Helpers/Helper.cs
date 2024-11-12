using System.Runtime.InteropServices;
using System.Text;
using Humanizer;
using MVCT.Sisfv.Tansversales.Notificaciones.Worker.Infrastructure.Configurations;

namespace MVCT.Sisfv.Tansversales.Notificaciones.Worker.Helpers
{
    public static class StringHumanize
    {
        public static string HumanizeCronSummary(string cronExpression)
        {
            var cron = new Quartz.CronExpression(cronExpression);

            var cronSummary = cron.GetExpressionSummary();
            // Split the summary into lines
            string[] lines = cronSummary.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            // Create a human-readable summary
            StringBuilder humanizedSummary = new StringBuilder();
            humanizedSummary.AppendLine(cronExpression);
            foreach (var line in lines)
            {
                string[] parts = line.Split(':', StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length == 2)
                {
                    string component = parts[0].Trim();
                    string expression = parts[1].Trim();

                    // Customize the humanization based on the component
                    string humanizedComponent = HumanizeComponent(component, expression);

                    humanizedSummary.AppendLine(humanizedComponent);
                }
            }

            return humanizedSummary.ToString().Trim();
        }

        public static string HumanizeComponent(string component, string expression)
        {
            // Customize the humanization based on the component
            switch (component)
            {
                case "seconds":
                case "minutes":
                case "hours":
                case "daysOfMonth":
                case "months":
                case "daysOfWeek":
                case "years":
                    return $"{component}: {expression.Humanize()}";
                case "lastdayOfWeek":
                case "nearestWeekday":
                case "NthDayOfWeek":
                case "lastdayOfMonth":
                case "calendardayOfWeek":
                case "calendardayOfMonth":
                    return $"{component}: {expression}";
                default:
                    return $"{component}: {expression}";
            }
        }
    }
    public class TemplateHelper
    {
        private IConfiguration _configuration;

        public TemplateHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Template GetTemplatePath(string taskJobName)
        {
            var mailEngineConfiguration = _configuration.GetSection("MailEngine");
            var templateConfigurationList = mailEngineConfiguration.GetValue<List<Template>>("Templates");


            var templateConfiguration = templateConfigurationList.FirstOrDefault(x => x.TaskJobName.ToString().Equals(taskJobName));

            return templateConfiguration;
        }
    }

    public static class OperatingSystem
    {
        public static bool IsWindows() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        public static bool IsMacOS() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

        public static bool IsLinux() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

        public static string GetTemplatesPath()
        {
            var environment = Environment.GetEnvironmentVariable("TEMPLATES");
            var templateFolder = string.Empty;

            if (environment is null)
            {
                if (IsLinux())
                {
                    templateFolder = "/app/Templates/";
                }

                if (IsWindows())
                {
                    templateFolder = $"{AppDomain.CurrentDomain.BaseDirectory}Templates\\";
                }
            }
            else
            {
                templateFolder = environment;
            }

            return templateFolder;
        }


    }
}
