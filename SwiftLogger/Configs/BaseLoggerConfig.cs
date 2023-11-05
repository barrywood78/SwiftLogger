using SwiftLogger.Enums;
using System.Collections.Generic;

public class BaseLoggerConfig<T> where T : BaseLoggerConfig<T>
{
    internal string TimestampFormat { get; private set; } = "yyyy-MM-dd HH:mm:ss";
    internal string MessageTemplate { get; private set; } = "{Timestamp} [{Level}] {Message}";
    internal LogLevel? MinimumLogLevel { get; private set; }
    internal bool IsLoggingEnabled { get; private set; } = true;
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

    public T SetMinimumLogLevel(LogLevel level)
    {
        MinimumLogLevel = level;
        return (T)this;
    }

    public T DisableLogging()
    {
        IsLoggingEnabled = false;
        return (T)this;
    }

    internal bool ShouldLog(LogLevel level)
    {
        if (!IsLoggingEnabled)
            return false;

        if (MinimumLogLevel.HasValue && level < MinimumLogLevel.Value)
            return false;

        return !IsLogLevelExcluded(level);
    }
}
