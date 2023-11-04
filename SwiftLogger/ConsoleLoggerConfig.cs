using SwiftLogger.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftLogger
{
    public class ConsoleLoggerConfig
    {
        public Dictionary<LogLevel, ConsoleColor> LogLevelColors { get; private set; } = new Dictionary<LogLevel, ConsoleColor>
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
    }

}
