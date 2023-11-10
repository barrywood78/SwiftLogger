#nullable enable

using SwiftLogger.Models;
using System.Threading.Tasks;

namespace SwiftLogger
{
    /// <summary>
    /// Represents a logger capable of logging <see cref="LogEvent"/> instances.
    /// This interface defines a fundamental contract for all loggers in the SwiftLogger system,
    /// ensuring they can handle asynchronous logging of log events.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Asynchronously logs the specified log event.
        /// Implementations of this method are responsible for handling the log event
        /// according to their specific logging mechanism (e.g., writing to a file, sending over the network).
        /// </summary>
        /// <param name="logEvent">The log event to be logged. This contains all relevant information about the event.</param>
        /// <returns>A task that represents the asynchronous logging operation.</returns>
        Task LogAsync(LogEvent logEvent);
    }
}
