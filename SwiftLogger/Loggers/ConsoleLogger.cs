using SwiftLogger.Configs;
using SwiftLogger.Models;
using System;
using System.Threading.Tasks;

namespace SwiftLogger.Loggers
{
    /// <summary>
    /// Provides methods to log messages to the console based on the provided configuration.
    /// </summary>
    internal class ConsoleLogger : ILogger
    {
        private readonly ConsoleLoggerConfig _config;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleLogger"/> class.
        /// </summary>
        /// <param name="config">The configuration to be used for logging. If null, a default configuration will be used.</param>
        public ConsoleLogger(ConsoleLoggerConfig config = null)
        {
            _config = config ?? new ConsoleLoggerConfig();
        }

        /// <summary>
        /// Logs the provided event to the console.
        /// </summary>
        /// <param name="logEvent">The event details to be logged.</param>
        /// <returns>A task that represents the asynchronous logging operation.</returns>
        public async Task LogAsync(LogEvent logEvent)
        {
            if (logEvent == null) throw new ArgumentNullException(nameof(logEvent));

            await Task.Yield();

            if (!_config.ShouldLog(logEvent.Level))
                return;

            ConsoleColor? previousColor = null;

            if (_config.LogLevelColors.TryGetValue(logEvent.Level, out var color))
            {
                previousColor = Console.ForegroundColor;
                Console.ForegroundColor = color;
            }

            try
            {
                var message = $"{logEvent.Timestamp.ToString(_config.TimestampFormat)} {logEvent.Level} {logEvent.Message}";
                Console.WriteLine(message);
            }
            catch (Exception ex)
            {
                // Handle unexpected logging errors.
                Console.Error.WriteLine($"Error logging message: {ex.Message}");
            }
            finally
            {
                if (previousColor.HasValue)
                    Console.ForegroundColor = previousColor.Value;
            }
        }
    }
}
