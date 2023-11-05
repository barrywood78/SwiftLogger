using SwiftLogger.Enums;
using System.Collections.Generic;

public class BaseLoggerConfig<T> where T : BaseLoggerConfig<T>
{
    internal string TimestampFormat { get; private set; } = "yyyy-MM-dd HH:mm:ss";
    internal string MessageTemplate { get; private set; } = "{Timestamp} [{Level}] {Message}";

    private HashSet<LogLevel> ExcludedLogLevels { get; } = new HashSet<LogLevel>();

    public T SetTimestampFormat(string format)
    {
        TimestampFormat = format;
        return (T)this;
    }

    public T SetMessageTemplate(string template)
    {
        MessageTemplate = template;
        return (T)this;
    }

    public T SetExcludeLogLevel(LogLevel level)
    {
        ExcludedLogLevels.Add(level);
        return (T)this;
    }

    internal bool IsLogLevelExcluded(LogLevel level)
    {
        return ExcludedLogLevels.Contains(level);
    }
}
