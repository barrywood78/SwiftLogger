using System.Net.Mail;
using SwiftLogger.Configs;
using SwiftLogger.Models;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace SwiftLogger.Loggers
{
    /// <summary>
    /// Provides functionality to log messages via email using SMTP, based on a provided configuration.
    /// Implements the <see cref="ILogger"/> interface and <see cref="IDisposable"/> to manage resources.
    /// </summary>
    internal class EmailLogger : ILogger, IDisposable
    {
        private readonly SmtpClient _smtpClient;
        private readonly EmailLoggerConfig _config;
        private bool _disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailLogger"/> class with the specified email logger configuration.
        /// </summary>
        /// <param name="config">The configuration settings for email logging.</param>
        public EmailLogger(EmailLoggerConfig config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _smtpClient = new SmtpClient(_config.SmtpServer, _config.SmtpPort)
            {
                EnableSsl = _config.UseSsl,
                Credentials = _config.Credentials
            };
        }

        /// <summary>
        /// Asynchronously logs a given log event by sending an email.
        /// </summary>
        /// <param name="logEvent">The log event details to be sent via email.</param>
        /// <returns>A task representing the asynchronous operation of sending the email.</returns>
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

            // Add email recipients and attachments
            AddRecipientsAndAttachments(mailMessage);

            try
            {
                await _smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }

        // Add recipients and attachments to the mail message
        private void AddRecipientsAndAttachments(MailMessage mailMessage)
        {
            foreach (var recipient in _config.GetToRecipients())
                mailMessage.To.Add(recipient);
            foreach (var recipient in _config.GetCcRecipients())
                mailMessage.CC.Add(recipient);
            foreach (var recipient in _config.GetBccRecipients())
                mailMessage.Bcc.Add(recipient);
            foreach (var path in _config.GetAttachmentPaths())
                mailMessage.Attachments.Add(new Attachment(path));
        }

        // Implement IDisposable to cleanup resources
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _smtpClient?.Dispose();
                }
                _disposed = true;
            }
        }
    }
}
