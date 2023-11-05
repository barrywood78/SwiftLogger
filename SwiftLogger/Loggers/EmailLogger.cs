using System.Net.Mail;
using SwiftLogger.Configs;
using SwiftLogger.Models;
using System;
using System.Threading.Tasks;

namespace SwiftLogger.Loggers
{
    /// <summary>
    /// Provides logging functionality that sends log events as emails based on the provided configuration.
    /// </summary>
    internal class EmailLogger : ILogger, IDisposable
    {
        private readonly SmtpClient _smtpClient;
        private readonly EmailLoggerConfig _config;
        private bool _disposed = false; // To detect redundant calls to Dispose

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailLogger"/> class.
        /// </summary>
        /// <param name="config">The email logger configuration.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="config"/> is null.</exception>
        public EmailLogger(EmailLoggerConfig config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));

            _smtpClient = new SmtpClient(_config.SmtpServer, _config.SmtpPort)
            {
                UseDefaultCredentials = _config.UseDefaultCredentials,
                EnableSsl = _config.UseSsl,
                Credentials = _config.Credentials
            };
        }

        /// <summary>
        /// Asynchronously logs the provided event as an email.
        /// </summary>
        /// <param name="logEvent">The event details to be logged.</param>
        /// <returns>A task that represents the asynchronous logging operation.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="logEvent"/> is null.</exception>
        public async Task LogAsync(LogEvent logEvent)
        {
            if (logEvent == null) throw new ArgumentNullException(nameof(logEvent));

            if (!_config.ShouldLog(logEvent.Level))
                return;

            var messageBody = $"{logEvent.Timestamp.ToString(_config.TimestampFormat)} {logEvent.Level} {logEvent.Message}";

            using var mailMessage = new MailMessage(_config.FromAddress, _config.ToAddress)
            {
                Subject = $"Log - {logEvent.Level}",
                Body = messageBody
            };

            try
            {
                await _smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="EmailLogger"/> and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _smtpClient.Dispose();
                }

                _disposed = true;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
