using SwiftLogger.Enums;
using System;
using System.Collections.Generic;

namespace SwiftLogger.Configs
{
    /// <summary>
    /// Represents a configuration for console logging.
    /// </summary>
    public class ConsoleLoggerConfig : BaseLoggerConfig<ConsoleLoggerConfig>
    {
        /// <summary>
        /// Gets the log level colors for console logging.
        /// </summary>
        internal readonly Dictionary<LogLevel, ConsoleColor> LogLevelColors = new()
        {
            [LogLevel.Trace] = ConsoleColor.White,
            [LogLevel.Debug] = ConsoleColor.White,
            [LogLevel.Information] = ConsoleColor.White,
            [LogLevel.Warning] = ConsoleColor.Yellow,
            [LogLevel.Error] = ConsoleColor.Red,
            [LogLevel.Critical] = ConsoleColor.Red
        };

        /// <summary>
        /// Sets the color for a specific log level in console logging.
        /// </summary>
        /// <param name="level">The log level.</param>
        /// <param name="color">The console color.</param>
        /// <returns>The console logger configuration.</returns>
        public ConsoleLoggerConfig SetColorForLogLevel(LogLevel level, ConsoleColor color)
        {
            LogLevelColors[level] = color;
            return this;
        }
    }
}
