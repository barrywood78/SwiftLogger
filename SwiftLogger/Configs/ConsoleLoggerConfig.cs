using SwiftLogger.Enums;
using System.Collections.Generic;
using System;

namespace SwiftLogger.Configs
{
    public class ConsoleLoggerConfig : BaseLoggerConfig<ConsoleLoggerConfig>
    {
        internal Dictionary<LogLevel, ConsoleColor> LogLevelColors { get; private set; } = new Dictionary<LogLevel, ConsoleColor>
        {
            { LogLevel.Debug, ConsoleColor.White },
            { LogLevel.Information, ConsoleColor.White },
            { LogLevel.Warning, ConsoleColor.Yellow },
            { LogLevel.Error, ConsoleColor.Red },
            { LogLevel.Critical, ConsoleColor.Red }
        };

        public ConsoleLoggerConfig SetColorForLogLevel(LogLevel level, ConsoleColor color)
        {
            LogLevelColors[level] = color;
            return this;
        }

        // Any additional methods or properties specific to ConsoleLoggerConfig can be added here.
    }
}
