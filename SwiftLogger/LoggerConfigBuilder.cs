#nullable enable

using System;
using System.Collections.Generic;

namespace SwiftLogger
{
    /// <summary>
    /// Provides functionality to build a configuration for a <see cref="SwiftLogger"/>.
    /// This class allows the addition of different loggers (e.g., console, file, email) to create a composite logger configuration.
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
        /// Gets the logger providers, facilitating the configuration of various logging targets.
        /// </summary>
        public LoggerProviders LogTo => _loggerProviders;

        /// <summary>
        /// Adds an <see cref="ILogger"/> instance to the configuration.
        /// </summary>
        /// <param name="logger">The logger to be added to the configuration. Must not be null.</param>
        internal void AddLogger(ILogger logger)
        {
            if (logger is null) throw new ArgumentNullException(nameof(logger));

            _loggers.Add(logger);
        }

        /// <summary>
        /// Builds and returns a <see cref="SwiftLogger"/> instance using the provided configuration.
        /// This method aggregates all the added loggers into a single <see cref="SwiftLogger"/> instance.
        /// </summary>
        /// <returns>A new <see cref="SwiftLogger"/> instance configured with the added loggers.</returns>
        public SwiftLogger Build()
        {
            return new SwiftLogger(_loggers);
        }
    }
}
