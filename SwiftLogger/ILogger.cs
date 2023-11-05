using SwiftLogger.Models;

namespace SwiftLogger
{
    public interface ILogger
    {
        Task Log(LogEvent logEvent);
    }
}
