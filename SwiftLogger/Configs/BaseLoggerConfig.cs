using SwiftLogger.Enums;
using System.Collections.Generic;

/// <summary>
/// Represents a base configuration for logging.
/// This generic class provides core configuration capabilities such as setting timestamp format,
/// message template, minimum log level, and enabling or disabling logging.
/// </summary>
/// <typeparam name="T">The type of the logger configuration, allowing for fluent-style method chaining.</typeparam>
public class BaseLoggerConfig<T> where T : BaseLoggerConfig<T>
{
    // The format used for timestamps in log messages.
    internal string TimestampFormat { get; private set; } = "yyyy-MM-dd HH:mm:ss";

    // The template for formatting log messages.
    internal string MessageTemplate { get; private set; } = "{Timestamp} [{Level}] {Message}";

    // The minimum log level required for a log message to be recorded.
    internal LogLevel? MinimumLogLevel { get; private set; }

    // Flag to enable or disable logging functionality.
    internal bool IsLoggingEnabled { get; private set; } = true;

    // A set of log levels that are excluded from logging.
    private HashSet<LogLevel> ExcludedLogLevels { get; } = new();

    /// <summary>
    /// Sets the timestamp format for logging.
    /// </summary>
    /// <param name="format">The desired timestamp format, e.g., "yyyy-MM-dd HH:mm:ss".</param>
    /// <returns>The logger configuration instance for method chaining.</returns>
    public T SetTimestampFormat(string format)
    {
        TimestampFormat = format;
        return (T)this;
    }

    /// <summary>
    /// Sets the message template for logging.
    /// </summary>
    /// <param name="template">The desired message template, e.g., "{Timestamp} [{Level}] {Message}".</param>
    /// <returns>The logger configuration instance for method chaining.</returns>
    public T SetMessageTemplate(string template)
    {
        MessageTemplate = template;
        return (T)this;
    }

    /// <summary>
    /// Excludes a specific log level from logging.
    /// This can be used to prevent logs of a certain level from being recorded.
    /// </summary>
    /// <param name="level">The log level to exclude.</param>
    /// <returns>The logger configuration instance for method chaining.</returns>
    public T SetExcludeLogLevel(LogLevel level)
    {
        ExcludedLogLevels.Add(level);
        return (T)this;
    }

    // Determines if a given log level is excluded from logging.
    internal bool IsLogLevelExcluded(LogLevel level) => ExcludedLogLevels.Contains(level);

    /// <summary>
    /// Sets the minimum log level for logging.
    /// Log messages below this level will not be recorded.
    /// </summary>
    /// <param name="level">The minimum log level to be set.</param>
    /// <returns>The logger configuration instance for method chaining.</returns>
    public T SetMinimumLogLevel(LogLevel level)
    {
        MinimumLogLevel = level;
        return (T)this;
    }

    /// <summary>
    /// Disables logging.
    /// This can be used to turn off logging dynamically, for instance, in performance-critical scenarios.
    /// </summary>
    /// <returns>The logger configuration instance for method chaining.</returns>
    public T DisableLogging()
    {
        IsLoggingEnabled = false;
        return (T)this;
    }

    // Determines whether a log message of a given level should be logged based on the current configuration.
    internal bool ShouldLog(LogLevel level)
    {
        return IsLoggingEnabled &&
               (MinimumLogLevel is null || level >= MinimumLogLevel.Value) &&
               !IsLogLevelExcluded(level);
    }
}
