
using SwiftLogger.Configs;
using SwiftLogger.Loggers;

namespace SwiftLogger
{
    public class LoggerDestinations
    {
        private readonly SwiftLoggerConfig _parent;

        internal LoggerDestinations(SwiftLoggerConfig parent)
        {
            _parent = parent;
        }

        public SwiftLoggerConfig Console(ConsoleLoggerConfig config = null)
        {
            _parent.AddLogger(new ConsoleLogger(config));
            return _parent;
        }

        public SwiftLoggerConfig File(FileLoggerConfig config = null)
        {
            _parent.AddLogger(new FileLogger(config));
            return _parent;
        }

        public SwiftLoggerConfig Email(EmailLoggerConfig config = null)
        {
            _parent.AddLogger(new EmailLogger(config));
            return _parent;
        }



    }

}
