#nullable enable

using SwiftLogger.Enums;
using System;

namespace SwiftLogger.Models
{
    /// <summary>
    /// Represents a single logging event with details about the log's timestamp, level, and message.
    /// </summary>
    public record LogEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogEvent"/> record.
        /// </summary>
        /// <param name="timestamp">The timestamp of the log event.</param>
        /// <param name="level">The severity level of the log event.</param>
        /// <param name="message">The message or content of the log event.</param>
        public LogEvent(DateTime timestamp, LogLevel level, string? message)
        {
            Timestamp = timestamp;
            Level = level;
            Message = message;
        }

        /// <summary>
        /// Gets the timestamp indicating when the log event occurred.
        /// </summary>
        public DateTime Timestamp { get; init; }

        /// <summary>
        /// Gets the severity level of the log event.
        /// </summary>
        public LogLevel Level { get; init; }

        /// <summary>
        /// Gets the actual message or content of the log event.
        /// </summary>
        public string? Message { get; init; }
    }
}
