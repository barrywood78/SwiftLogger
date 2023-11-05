#nullable enable

using SwiftLogger.Models;
using System.Threading.Tasks;

namespace SwiftLogger
{
    /// <summary>
    /// Represents a logger capable of logging <see cref="LogEvent"/> instances.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Asynchronously logs the specified log event.
        /// </summary>
        /// <param name="logEvent">The log event to be logged.</param>
        /// <returns>A task that represents the asynchronous logging operation.</returns>
        Task LogAsync(LogEvent logEvent);
    }
}
