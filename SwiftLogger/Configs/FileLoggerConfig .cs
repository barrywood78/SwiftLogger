using System;

namespace SwiftLogger.Configs
{
    /// <summary>
    /// Represents a configuration for file logging.
    /// This class extends <see cref="BaseLoggerConfig{T}"/> and provides additional settings specific to file logging,
    /// such as file path, maximum file size, and the option to separate logs by date.
    /// </summary>
    public sealed class FileLoggerConfig : BaseLoggerConfig<FileLoggerConfig>
    {
        // Path to the log file.
        internal string FilePath { get; private set; } = "log.txt";

        // Maximum size of the log file in bytes. Null indicates no limit.
        internal long? MaxFileSizeInBytes { get; private set; }

        // Whether to separate log entries by date.
        internal bool SeparateByDate { get; private set; } = false;

        /// <summary>
        /// Sets the file path for logging.
        /// </summary>
        /// <param name="path">The file path where logs will be written.</param>
        /// <returns>The file logger configuration instance for method chaining.</returns>
        public FileLoggerConfig SetFilePath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path), "File path cannot be null or whitespace.");
            }

            FilePath = path;
            return this;
        }

        /// <summary>
        /// Sets the maximum file size for logging.
        /// If the log file reaches this size, it can trigger a rollover event.
        /// </summary>
        /// <param name="sizeInBytes">The maximum size of the log file in bytes.</param>
        /// <returns>The file logger configuration instance for method chaining.</returns>
        public FileLoggerConfig SetMaxFileSize(long sizeInBytes)
        {
            if (sizeInBytes <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sizeInBytes), "Size in bytes must be a positive value.");
            }

            MaxFileSizeInBytes = sizeInBytes;
            return this;
        }

        /// <summary>
        /// Enables separation of log entries by date.
        /// When enabled, logs for each date can be written to separate files.
        /// </summary>
        /// <returns>The file logger configuration instance for method chaining.</returns>
        public FileLoggerConfig EnableSeparationByDate()
        {
            SeparateByDate = true;
            return this;
        }
    }
}
