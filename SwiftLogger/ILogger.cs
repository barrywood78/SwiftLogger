using SwiftLogger.Models;

namespace SwiftLogger
{
    public interface ILogger
    {
        void Log(LogEvent logEvent);
    }

}
