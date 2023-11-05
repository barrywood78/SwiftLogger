using SwiftLogger.Enums;
using System.Collections.Generic;

/// <summary>
/// Represents a base configuration for logging.
/// </summary>
/// <typeparam name="T">The type of the logger configuration.</typeparam>
public class BaseLoggerConfig<T> where T : BaseLoggerConfig<T>
{
    internal string TimestampFormat { get; private set; } = "yyyy-MM-dd HH:mm:ss";
    internal string MessageTemplate { get; private set; } = "{Timestamp} [{Level}] {Message}";
    internal LogLevel? MinimumLogLevel { get; private set; }
    internal bool IsLoggingEnabled { get; private set; } = true;

    private HashSet<LogLevel> ExcludedLogLevels { get; } = new();

    /// <summary>
    /// Sets the timestamp format for logging.
    /// </summary>
    /// <param name="format">The timestamp format.</param>
    /// <returns>The logger configuration.</returns>
    public T SetTimestampFormat(string format)
    {
        TimestampFormat = format;
        return (T)this;
    }

    /// <summary>
    /// Sets the message template for logging.
    /// </summary>
    /// <param name="template">The message template.</param>
    /// <returns>The logger configuration.</returns>
    public T SetMessageTemplate(string template)
    {
        MessageTemplate = template;
        return (T)this;
    }

    /// <summary>
    /// Excludes a log level from logging.
    /// </summary>
    /// <param name="level">The log level to exclude.</param>
    /// <returns>The logger configuration.</returns>
    public T SetExcludeLogLevel(LogLevel level)
    {
        ExcludedLogLevels.Add(level);
        return (T)this;
    }

    internal bool IsLogLevelExcluded(LogLevel level) => ExcludedLogLevels.Contains(level);

    /// <summary>
    /// Sets the minimum log level for logging.
    /// </summary>
    /// <param name="level">The minimum log level.</param>
    /// <returns>The logger configuration.</returns>
    public T SetMinimumLogLevel(LogLevel level)
    {
        MinimumLogLevel = level;
        return (T)this;
    }

    /// <summary>
    /// Disables logging.
    /// </summary>
    /// <returns>The logger configuration.</returns>
    public T DisableLogging()
    {
        IsLoggingEnabled = false;
        return (T)this;
    }

    internal bool ShouldLog(LogLevel level)
    {
        return IsLoggingEnabled &&
               (MinimumLogLevel is null || level >= MinimumLogLevel.Value) &&
               !IsLogLevelExcluded(level);
    }
}
