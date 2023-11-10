using SwiftLogger.Configs;
using SwiftLogger.Models;
using System;
using System.Threading.Tasks;

namespace SwiftLogger.Loggers
{
    /// <summary>
    /// Provides methods to log messages to the console based on the provided configuration.
    /// This logger is part of the SwiftLogger system and handles the console output of log messages.
    /// </summary>
    internal class ConsoleLogger : ILogger
    {
        private readonly ConsoleLoggerConfig _config;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleLogger"/> class with the specified configuration.
        /// </summary>
        /// <param name="config">The configuration to be used for logging. If null, a default configuration will be used.</param>
        public ConsoleLogger(ConsoleLoggerConfig? config = null)
        {
            _config = config ?? new ConsoleLoggerConfig();
        }

        /// <summary>
        /// Asynchronously logs the provided event to the console.
        /// </summary>
        /// <param name="logEvent">The event details to be logged. Must not be null.</param>
        /// <returns>A task representing the asynchronous logging operation.</returns>
        public async Task LogAsync(LogEvent logEvent)
        {
            if (logEvent == null) throw new ArgumentNullException(nameof(logEvent));

            await Task.Yield(); // Yield to ensure asynchronous execution.

            if (!_config.ShouldLog(logEvent.Level))
                return; // Skip logging if the level is not enabled.

            ConsoleColor? previousColor = null;

            // Check if a specific color is configured for this log level and apply it.
            if (_config.LogLevelColors.TryGetValue(logEvent.Level, out var color))
            {
                previousColor = Console.ForegroundColor;
                Console.ForegroundColor = color;
            }

            try
            {
                // Format and write the log message to the console.
                var message = _config.MessageTemplate
                    .Replace("{Timestamp}", logEvent.Timestamp.ToString(_config.TimestampFormat))
                    .Replace("{Level}", logEvent.Level.ToString())
                    .Replace("{Message}", logEvent.Message);

                Console.WriteLine(message);
            }
            catch (Exception ex)
            {
                // Handle unexpected errors during logging.
                Console.Error.WriteLine($"Error logging message: {ex.Message}");
            }
            finally
            {
                // Restore the original console color.
                if (previousColor.HasValue)
                    Console.ForegroundColor = previousColor.Value;
            }
        }
    }
}
