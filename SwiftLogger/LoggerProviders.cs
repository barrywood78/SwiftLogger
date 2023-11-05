
using SwiftLogger.Configs;
using SwiftLogger.Loggers;

namespace SwiftLogger
{
    public class LoggerProviders
    {
        private readonly LoggerConfigBuilder _parent;

        internal LoggerProviders(LoggerConfigBuilder parent)
        {
            _parent = parent;
        }

        public LoggerConfigBuilder Console(ConsoleLoggerConfig config = null)
        {
            _parent.AddLogger(new ConsoleLogger(config));
            return _parent;
        }

        public LoggerConfigBuilder File(FileLoggerConfig config = null)
        {
            _parent.AddLogger(new FileLogger(config));
            return _parent;
        }

        public LoggerConfigBuilder Email(EmailLoggerConfig config = null)
        {
            _parent.AddLogger(new EmailLogger(config));
            return _parent;
        }



    }

}
