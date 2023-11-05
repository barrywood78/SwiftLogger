using System.Net.Mail;
using SwiftLogger.Configs;
using SwiftLogger.Models;
using System.Threading.Tasks;

namespace SwiftLogger.Loggers
{
    internal class EmailLogger : ILogger
    {
        private readonly SmtpClient _smtpClient;
        private readonly EmailLoggerConfig _config;

        public EmailLogger(EmailLoggerConfig config)
        {
            _config = config;

            _smtpClient = new SmtpClient(_config.SmtpServer, _config.SmtpPort)
            {
                UseDefaultCredentials = _config.UseDefaultCredentials,
                EnableSsl = _config.UseSsl,
                Credentials = _config.Credentials
            };
        }

        public Task Log(LogEvent logEvent)
        {
            if (_config.IsLogLevelExcluded(logEvent.Level))
                return Task.CompletedTask;

            var messageBody = _config.MessageTemplate
                .Replace("{Timestamp}", logEvent.Timestamp.ToString(_config.TimestampFormat))
                .Replace("{Level}", logEvent.Level.ToString())
                .Replace("{Message}", logEvent.Message);

            var mailMessage = new MailMessage(_config.FromAddress, _config.ToAddress)
            {
                Subject = $"Log - {logEvent.Level}",
                Body = messageBody
            };

            try
            {
                _smtpClient.Send(mailMessage);
            }
            catch
            {
                // You might want to handle email sending failures here.
                // Perhaps retry, log to another logger, etc.
            }

            return Task.CompletedTask;
        }
    }
}
