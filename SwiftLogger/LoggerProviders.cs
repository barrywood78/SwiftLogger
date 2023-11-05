#nullable enable

using SwiftLogger.Configs;
using SwiftLogger.Loggers;

namespace SwiftLogger
{
    /// <summary>
    /// Provides methods to add different logger providers to a <see cref="LoggerConfigBuilder"/>.
    /// </summary>
    public sealed class LoggerProviders
    {
        private readonly LoggerConfigBuilder _parent;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerProviders"/> class.
        /// </summary>
        /// <param name="parent">The parent <see cref="LoggerConfigBuilder"/> to which loggers will be added.</param>
        internal LoggerProviders(LoggerConfigBuilder parent)
        {
            _parent = parent ?? throw new ArgumentNullException(nameof(parent));
        }

        /// <summary>
        /// Adds a console logger to the parent configuration.
        /// </summary>
        /// <param name="config">Optional configuration for the console logger.</param>
        /// <returns>The parent <see cref="LoggerConfigBuilder"/> to continue configuration.</returns>
        public LoggerConfigBuilder Console(ConsoleLoggerConfig? config = null)
        {
            _parent.AddLogger(new ConsoleLogger(config));
            return _parent;
        }

        /// <summary>
        /// Adds a file logger to the parent configuration.
        /// </summary>
        /// <param name="config">Optional configuration for the file logger.</param>
        /// <returns>The parent <see cref="LoggerConfigBuilder"/> to continue configuration.</returns>
        public LoggerConfigBuilder File(FileLoggerConfig? config = null)
        {
            _parent.AddLogger(new FileLogger(config));
            return _parent;
        }

        /// <summary>
        /// Adds an email logger to the parent configuration.
        /// </summary>
        /// <param name="config">Optional configuration for the email logger.</param>
        /// <returns>The parent <see cref="LoggerConfigBuilder"/> to continue configuration.</returns>
        public LoggerConfigBuilder Email(EmailLoggerConfig? config = null)
        {
            _parent.AddLogger(new EmailLogger(config));
            return _parent;
        }
    }
}
