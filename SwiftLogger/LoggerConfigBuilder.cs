
namespace SwiftLogger
{
    public class LoggerConfigBuilder
    {
        private readonly List<ILogger> _loggers = new List<ILogger>();

        public LoggerProviders LogTo => new LoggerProviders(this);

        internal void AddLogger(ILogger logger)
        {
            _loggers.Add(logger);
        }

 
        public SwiftLogger Build()
        {
            return new SwiftLogger(_loggers);
        }
    }

}
