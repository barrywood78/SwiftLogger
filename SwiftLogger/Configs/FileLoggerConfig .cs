using System;

namespace SwiftLogger.Configs
{
    /// <summary>
    /// Represents a configuration for file logging.
    /// </summary>
    public sealed class FileLoggerConfig : BaseLoggerConfig<FileLoggerConfig>
    {
        /// <summary>
        /// Gets the file path for logging.
        /// </summary>
        internal string FilePath { get; private set; } = "log.txt";

        /// <summary>
        /// Gets the maximum file size in bytes for logging.
        /// A null value indicates no size limit.
        /// </summary>
        internal long? MaxFileSizeInBytes { get; private set; }

        /// <summary>
        /// Gets a value indicating whether logs should be separated by date.
        /// </summary>
        internal bool SeparateByDate { get; private set; } = false;

        /// <summary>
        /// Sets the file path for logging.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <returns>The file logger configuration.</returns>
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
        /// </summary>
        /// <param name="sizeInBytes">The maximum size in bytes.</param>
        /// <returns>The file logger configuration.</returns>
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
        /// Enables separation of logs by date.
        /// </summary>
        /// <returns>The file logger configuration.</returns>
        public FileLoggerConfig EnableSeparationByDate()
        {
            SeparateByDate = true;
            return this;
        }
    }
}
