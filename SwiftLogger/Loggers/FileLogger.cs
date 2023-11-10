using SwiftLogger.Configs;
using SwiftLogger.Models;
using System.IO;
using System.Threading.Tasks;

namespace SwiftLogger
{
    /// <summary>
    /// Provides methods to log messages to a file based on the provided configuration.
    /// This logger handles writing log messages to a file, managing aspects like file path, size, and format.
    /// </summary>
    public class FileLogger : ILogger
    {
        private readonly FileLoggerConfig _config;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileLogger"/> class with the specified configuration.
        /// </summary>
        /// <param name="config">The configuration for the file logger. If null, default settings are used.</param>
        public FileLogger(FileLoggerConfig config = null)
        {
            _config = config ?? new FileLoggerConfig();
        }

        /// <summary>
        /// Asynchronously logs a message to a file using the provided log event details.
        /// </summary>
        /// <param name="logEvent">The event details to be logged. Must not be null.</param>
        /// <returns>A task representing the asynchronous logging operation.</returns>
        public async Task LogAsync(LogEvent logEvent)
        {
            if (!_config.ShouldLog(logEvent.Level))
                return; // Skip logging if the log level is not enabled.

            var message = _config.MessageTemplate
                .Replace("{Timestamp}", logEvent.Timestamp.ToString(_config.TimestampFormat))
                .Replace("{Level}", logEvent.Level.ToString())
                .Replace("{Message}", logEvent.Message);

            string targetFilePath = GetTargetFilePath();

            await using var stream = new FileStream(targetFilePath, FileMode.Append, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true);
            using var writer = new StreamWriter(stream);
            await writer.WriteLineAsync(message);
        }

        /// <summary>
        /// Determines the target file path for logging based on the configuration settings.
        /// This method considers settings like separation by date and maximum file size to generate the appropriate file path.
        /// </summary>
        /// <returns>The computed target file path for logging.</returns>
        private string GetTargetFilePath()
        {
            string baseFilePath = _config.FilePath;
            string directory = Path.GetDirectoryName(baseFilePath);
            string fileName = Path.GetFileNameWithoutExtension(baseFilePath);
            string extension = Path.GetExtension(baseFilePath);

            if (_config.SeparateByDate)
            {
                fileName += $"_{DateTime.Now:yyyyMMdd}";
            }

            if (_config.MaxFileSizeInBytes.HasValue)
            {
                int counter = 0;
                while (true)
                {
                    string tempFileName = $"{fileName}{(counter > 0 ? $"_{counter}" : "")}";
                    string tempFilePath = Path.Combine(directory, $"{tempFileName}{extension}");
                    if (!File.Exists(tempFilePath) || new FileInfo(tempFilePath).Length < _config.MaxFileSizeInBytes.Value)
                    {
                        return tempFilePath;
                    }
                    counter++;
                }
            }

            return Path.Combine(directory, $"{fileName}{extension}");
        }
    }
}
