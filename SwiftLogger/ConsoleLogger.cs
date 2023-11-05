using SwiftLogger.Models;
using System;

namespace SwiftLogger
{
    internal class ConsoleLogger : ILogger
    {
        private readonly ConsoleLoggerConfig _config;

        public ConsoleLogger(ConsoleLoggerConfig config = null)
        {
            _config = config ?? new ConsoleLoggerConfig(); // Use provided config or default to new config.
        }

        public void Log(LogEvent logEvent)
        {
            if (_config.IsLogLevelExcluded(logEvent.Level))
                return;

            if (_config.LogLevelColors.TryGetValue(logEvent.Level, out var color))
            {
                Console.ForegroundColor = color;
            }

            var message = _config.MessageTemplate
                .Replace("{Timestamp}", logEvent.Timestamp.ToString(_config.TimestampFormat))
                .Replace("{Level}", logEvent.Level.ToString())
                .Replace("{Message}", logEvent.Message);

            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
