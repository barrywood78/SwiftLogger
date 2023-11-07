using System.Net.Mail;
using SwiftLogger.Configs;
using SwiftLogger.Models;
using System;
using System.Threading.Tasks;

namespace SwiftLogger.Loggers
{
    internal class EmailLogger : ILogger, IDisposable
    {
        private readonly SmtpClient _smtpClient;
        private readonly EmailLoggerConfig _config;
        private bool _disposed = false;

        public EmailLogger(EmailLoggerConfig config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _smtpClient = new SmtpClient(_config.SmtpServer, _config.SmtpPort)
            {
                EnableSsl = _config.UseSsl,
                Credentials = _config.Credentials
            };
        }

        public async Task LogAsync(LogEvent logEvent)
        {
            if (logEvent == null) throw new ArgumentNullException(nameof(logEvent));
            if (!_config.ShouldLog(logEvent.Level)) return;

            var messageBody = _config.MessageTemplate
                .Replace("{Timestamp}", logEvent.Timestamp.ToString(_config.TimestampFormat))
                .Replace("{Level}", logEvent.Level.ToString())
                .Replace("{Message}", logEvent.Message);

            var formattedSubject = (_config.SubjectFormat ?? "Log")
                .Replace("{Timestamp}", logEvent.Timestamp.ToString(_config.TimestampFormat))
                .Replace("{Level}", logEvent.Level.ToString())
                .Replace("{Message}", logEvent.Message);

            using var mailMessage = new MailMessage
            {
                From = new MailAddress(_config.FromAddress ?? string.Empty),
                Subject = formattedSubject,
                Body = messageBody
            };

            // Add To recipients
            foreach (var recipient in _config.GetToRecipients())
            {
                mailMessage.To.Add(recipient);
            }

            // Add CC recipients
            foreach (var recipient in _config.GetCcRecipients())
            {
                mailMessage.CC.Add(recipient);
            }

            // Add BCC recipients
            foreach (var recipient in _config.GetBccRecipients())
            {
                mailMessage.Bcc.Add(recipient);
            }

            // Add attachments
            var attachments = _config.GetAttachmentPaths().Select(path => new Attachment(path)).ToList();
            foreach (var attachment in attachments)
            {
                mailMessage.Attachments.Add(attachment);
            }

            try
            {
                await _smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _smtpClient.Dispose();
                    // Dispose of any disposable attachments if not using a using statement.
                    foreach (var attachment in _config.GetCurrentAttachments())
                    {
                        attachment.Dispose();
                    }
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
