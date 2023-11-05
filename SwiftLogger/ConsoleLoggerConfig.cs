using SwiftLogger.Enums;
using System.Collections.Generic;

namespace SwiftLogger
{
    public class ConsoleLoggerConfig
    {
        internal Dictionary<LogLevel, ConsoleColor> LogLevelColors { get; private set; } = new Dictionary<LogLevel, ConsoleColor>
        {
            { LogLevel.Debug, ConsoleColor.White },
            { LogLevel.Information, ConsoleColor.White },
            { LogLevel.Warning, ConsoleColor.Yellow },
            { LogLevel.Error, ConsoleColor.Red },
            { LogLevel.Critical, ConsoleColor.Red }
        };

        internal string TimestampFormat { get; private set; } = "yyyy-MM-dd HH:mm:ss";
        internal string MessageTemplate { get; private set; } = "{Timestamp} [{Level}] {Message}";
        private readonly HashSet<LogLevel> excludedLogLevels = new HashSet<LogLevel>();

        public ConsoleLoggerConfig SetTimestampFormat(string format)
        {
            TimestampFormat = format;
            return this;
        }

        public ConsoleLoggerConfig SetMessageTemplate(string template)
        {
            MessageTemplate = template;
            return this;
        }

        public ConsoleLoggerConfig SetColorForLogLevel(LogLevel level, ConsoleColor color)
        {
            LogLevelColors[level] = color;
            return this;
        }

        public ConsoleLoggerConfig SetExcludeLogLevel(LogLevel level)
        {
            excludedLogLevels.Add(level);
            return this;
        }

        internal bool IsLogLevelExcluded(LogLevel level)
        {
            return excludedLogLevels.Contains(level);
        }
    }
}
