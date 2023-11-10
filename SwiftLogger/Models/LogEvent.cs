#nullable enable

using SwiftLogger.Enums;
using System;

namespace SwiftLogger.Models
{
    /// <summary>
    /// Represents a single logging event, encapsulating details about the event's timestamp, severity level, and message.
    /// This record is used to capture and transport logging information throughout the logging system.
    /// </summary>
    public record LogEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogEvent"/> record.
        /// </summary>
        /// <param name="timestamp">The timestamp indicating when the log event occurred.</param>
        /// <param name="level">The severity level of the log event, determining the urgency and importance of the event.</param>
        /// <param name="message">The message or content of the log event, providing details about what occurred.</param>
        public LogEvent(DateTime timestamp, LogLevel level, string? message)
        {
            Timestamp = timestamp;
            Level = level;
            Message = message;
        }

        /// <summary>
        /// Gets the timestamp indicating when the log event occurred.
        /// This property is crucial for understanding the sequence and timing of logged events.
        /// </summary>
        public DateTime Timestamp { get; init; }

        /// <summary>
        /// Gets the severity level of the log event.
        /// The level is used to categorize the event's importance and can influence how it is processed and where it is stored.
        /// </summary>
        public LogLevel Level { get; init; }

        /// <summary>
        /// Gets the actual message or content of the log event.
        /// This property contains the descriptive information regarding the event being logged.
        /// </summary>
        public string? Message { get; init; }
    }
}
