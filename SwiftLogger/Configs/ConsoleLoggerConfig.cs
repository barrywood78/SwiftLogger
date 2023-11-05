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
            { LogLevel.Info, ConsoleColor.White },
            { LogLevel.Warn, ConsoleColor.Yellow },
            { LogLevel.Error, ConsoleColor.Red },
            { LogLevel.Critical, ConsoleColor.Red },
            { LogLevel.Exception, ConsoleColor.Red },
            { LogLevel.Alert, ConsoleColor.Red }
        };

        public ConsoleLoggerConfig SetColorForLogLevel(LogLevel level, ConsoleColor color)
        {
            LogLevelColors[level] = color;
            return this;
        }

        // Any additional methods or properties specific to ConsoleLoggerConfig can be added here.
    }
}
