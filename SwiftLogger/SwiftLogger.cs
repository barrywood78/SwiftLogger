using SwiftLogger.Enums;
using SwiftLogger.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SwiftLogger
{
    public class SwiftLogger
    {
        private readonly List<ILogger> _loggers;

        public SwiftLogger(List<ILogger> loggers)
        {
            _loggers = loggers;
        }

        public async Task Log(LogLevel level, string message)
        {
            var logEvent = new LogEvent
            {
                Timestamp = DateTime.UtcNow,
                Level = level,
                Message = message
            };

            foreach (var logger in _loggers)
            {
                await logger.Log(logEvent);
            }
        }
    }
}
