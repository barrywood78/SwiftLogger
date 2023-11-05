using SwiftLogger.Enums;
using SwiftLogger.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SwiftLogger
{
    /// <summary>
    /// Represents a logger that can log to multiple underlying loggers.
    /// </summary>
    public sealed class SwiftLogger
    {
        private readonly IReadOnlyList<ILogger> _loggers;

        /// <summary>
        /// Initializes a new instance of the <see cref="SwiftLogger"/> class.
        /// </summary>
        /// <param name="loggers">A list of loggers to which log events will be sent.</param>
        public SwiftLogger(IReadOnlyList<ILogger> loggers)
        {
            _loggers = loggers ?? throw new ArgumentNullException(nameof(loggers));
        }

        /// <summary>
        /// Logs a message with the specified log level.
        /// </summary>
        /// <param name="level">The level of the log.</param>
        /// <param name="message">The message to be logged.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task Log(LogLevel level, string message)
        {
            var logEvent = new LogEvent(DateTime.Now, level, message);
            await LogToAll(logEvent);

            async Task LogToAll(LogEvent evt)
            {
                var tasks = new List<Task>();
                foreach (var logger in _loggers)
                {
                    tasks.Add(logger.LogAsync(evt));
                }
                await Task.WhenAll(tasks);
            }
        }
    }
}
