using SwiftLogger.Enums;
using System.Collections.Generic;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics;

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
            { LogLevel.Critical, ConsoleColor.Red },
        };

        public ConsoleLoggerConfig SetColorForLogLevel(LogLevel level, ConsoleColor color)
        {
            LogLevelColors[level] = color;
            return this;
        }

    }
}
