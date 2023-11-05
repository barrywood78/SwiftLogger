#nullable enable

using System;
using System.Collections.Generic;

namespace SwiftLogger
{
    /// <summary>
    /// Provides functionality to build a configuration for a <see cref="SwiftLogger"/>.
    /// </summary>
    public sealed class LoggerConfigBuilder
    {
        private readonly List<ILogger> _loggers = new();
        private readonly LoggerProviders _loggerProviders;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerConfigBuilder"/> class.
        /// </summary>
        public LoggerConfigBuilder()
        {
            _loggerProviders = new LoggerProviders(this);
        }

        /// <summary>
        /// Gets the logger providers to configure logging targets.
        /// </summary>
        public LoggerProviders LogTo => _loggerProviders;

        /// <summary>
        /// Adds an <see cref="ILogger"/> instance to the configuration.
        /// </summary>
        /// <param name="logger">The logger to be added.</param>
        internal void AddLogger(ILogger logger)
        {
            if (logger is null) throw new ArgumentNullException(nameof(logger));

            _loggers.Add(logger);
        }

        /// <summary>
        /// Builds and returns a <see cref="SwiftLogger"/> instance using the provided configuration.
        /// </summary>
        /// <returns>A new <see cref="SwiftLogger"/> instance.</returns>
        public SwiftLogger Build()
        {
            return new SwiftLogger(_loggers);
        }
    }
}
