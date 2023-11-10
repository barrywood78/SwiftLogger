using SwiftLogger.Enums;
using System;
using System.Collections.Generic;

namespace SwiftLogger.Configs
{
    /// <summary>
    /// Represents a configuration for console logging.
    /// Extends the <see cref="BaseLoggerConfig{T}"/> with console-specific features such as setting log level colors.
    /// </summary>
    public class ConsoleLoggerConfig : BaseLoggerConfig<ConsoleLoggerConfig>
    {
        // A dictionary mapping log levels to their corresponding console colors.
        // This allows for visual differentiation of log levels when output to the console.
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
        /// This method allows customization of how different log levels are visually represented in the console.
        /// </summary>
        /// <param name="level">The log level to set the color for.</param>
        /// <param name="color">The console color to be used for the specified log level.</param>
        /// <returns>The console logger configuration instance for method chaining.</returns>
        public ConsoleLoggerConfig SetColorForLogLevel(LogLevel level, ConsoleColor color)
        {
            LogLevelColors[level] = color;
            return this;
        }
    }
}
