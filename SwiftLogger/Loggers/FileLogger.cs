using SwiftLogger.Configs;
using SwiftLogger.Models;
using System.IO;
using System.Threading.Tasks;

namespace SwiftLogger
{
    public class FileLogger : ILogger
    {
        private readonly FileLoggerConfig _config;

        public FileLogger(FileLoggerConfig config = null)
        {
            _config = config ?? new FileLoggerConfig();
        }

        public async Task Log(LogEvent logEvent)
        {
            if (!_config.ShouldLog(logEvent.Level))
                return;

            var message = _config.MessageTemplate
                .Replace("{Timestamp}", logEvent.Timestamp.ToString(_config.TimestampFormat))
                .Replace("{Level}", logEvent.Level.ToString())
                .Replace("{Message}", logEvent.Message);

            string targetFilePath = GetTargetFilePath();

            using (var stream = new FileStream(targetFilePath, FileMode.Append, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true))
            using (var writer = new StreamWriter(stream))
            {
                await writer.WriteLineAsync(message);
            }
        }


        private string GetTargetFilePath()
        {
            string baseFilePath = _config.FilePath;
            string directory = Path.GetDirectoryName(baseFilePath);
            string fileName = Path.GetFileNameWithoutExtension(baseFilePath);
            string extension = Path.GetExtension(baseFilePath);

            if (_config.SeparateByDate)
            {
                fileName += $"_{DateTime.UtcNow:yyyyMMdd}";
            }

            if (_config.MaxFileSizeInBytes.HasValue)
            {
                int counter = 0;
                string tempFileName;
                while (true)
                {
                    tempFileName = fileName + (counter > 0 ? $"_{counter}" : "");
                    string tempFilePath = Path.Combine(directory, tempFileName + extension);
                    if (!File.Exists(tempFilePath) || new FileInfo(tempFilePath).Length < _config.MaxFileSizeInBytes.Value)
                    {
                        return tempFilePath;
                    }
                    counter++;
                }
            }

            return Path.Combine(directory, fileName + extension);
        }







    }
}
