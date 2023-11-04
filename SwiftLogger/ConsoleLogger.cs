using System;
using SwiftLogger.Enums;
using SwiftLogger.Models;

namespace SwiftLogger
{
    /// <summary>
    /// Provides logging capabilities to the console.
    /// </summary>
    public class ConsoleLogger : ILogger
    {
        private readonly ConsoleLoggerConfig _config;

        public ConsoleLogger(ConsoleLoggerConfig config = null)
        {
            _config = config ?? new ConsoleLoggerConfig(); // Use provided config or default to new config.
        }

        public void Log(LogEvent logEvent)
        {
            if (_config.LogLevelColors.TryGetValue(logEvent.Level, out var color))
            {
                Console.ForegroundColor = color;
            }
            Console.WriteLine($"{logEvent.Timestamp:yyyy-MM-dd HH:mm:ss} [{logEvent.Level}] {logEvent.Message}");
            Console.ResetColor();
        }
    }


}
