using System;

namespace Archivos.Domain.Common;

public class AppSettings
{
    public const string SectionKey = "ConnectionStrings";

    public string ConnectionString { get; set; } = string.Empty;

}
