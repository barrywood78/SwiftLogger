#nullable enable

using System.Net;
using System.Net.Mail;
using SwiftLogger.Enums;

namespace SwiftLogger.Configs
{
    /// <summary>
    /// Represents a configuration for email logging.
    /// </summary>
    public class EmailLoggerConfig : BaseLoggerConfig<EmailLoggerConfig>
    {
        // SMTP server details
        internal string? SmtpServer { get; private set; }
        internal int SmtpPort { get; private set; }
        internal bool UseSsl { get; private set; }

        // Authentication details
        internal NetworkCredential? Credentials { get; private set; }

        // Email details
        internal string? FromAddress { get; private set; }
        internal List<string> Recipients { get; } = new();
        internal string? SubjectFormat { get; private set; }
        internal List<AttachmentSource> Attachments { get; } = new List<AttachmentSource>();

        /// <summary>
        /// Sets the SMTP server for email logging.
        /// </summary>
        /// <param name="server">The SMTP server.</param>
        /// <returns>The email logger configuration.</returns>
        public EmailLoggerConfig SetSmtpServer(string server)
        {
            if (string.IsNullOrWhiteSpace(server))
                throw new ArgumentException("SMTP server cannot be null or whitespace.", nameof(server));

            SmtpServer = server;
            return this;
        }

        /// <summary>
        /// Sets the SMTP port for email logging.
        /// </summary>
        /// <param name="port">The SMTP port.</param>
        /// <returns>The email logger configuration.</returns>
        public EmailLoggerConfig SetSmtpPort(int port)
        {
            if (port <= 0 || port > 65535)
                throw new ArgumentOutOfRangeException(nameof(port), "Port number must be between 1 and 65535.");

            SmtpPort = port;
            return this;
        }

        /// <summary>
        /// Configures the usage of SSL for SMTP.
        /// </summary>
        /// <param name="useSsl">Flag indicating whether to use SSL. Default is true.</param>
        /// <returns>The email logger configuration.</returns>
        public EmailLoggerConfig UseSecureSocketLayer(bool useSsl = true)
        {
            UseSsl = useSsl;
            return this;
        }

        /// <summary>
        /// Sets the authentication credentials for SMTP.
        /// </summary>
        /// <param name="email">The email used for authentication.</param>
        /// <param name="password">The password used for authentication.</param>
        /// <returns>The email logger configuration.</returns>
        public EmailLoggerConfig SetAuthentication(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Email or password cannot be null or whitespace.");

            Credentials = new NetworkCredential(email, password);
            return this;
        }

        
        /// <summary>
        /// Sets the sender's email address for email logging.
        /// </summary>
        /// <param name="from">The sender's email address.</param>
        /// <returns>The email logger configuration.</returns>
        public EmailLoggerConfig SetFromAddress(string from)
        {
            if (string.IsNullOrWhiteSpace(from))
                throw new ArgumentException("From address cannot be null or whitespace.", nameof(from));

            FromAddress = from;
            return this;
        }

        public EmailLoggerConfig SetSubjectFormat(string format)
        {
            SubjectFormat = format;
            return this;
        }

        public EmailLoggerConfig AddRecipient(string recipient)
        {
            if (string.IsNullOrWhiteSpace(recipient))
                throw new ArgumentException("Recipient address cannot be null or whitespace.", nameof(recipient));

            Recipients.Add(recipient);
            return this;
        }

        public EmailLoggerConfig AddAttachment(string filePath)
        {
            Attachments.Add(AttachmentSource.FromFilePath(filePath));
            return this;
        }

        public EmailLoggerConfig AddAttachment(Stream stream)
        {
            Attachments.Add(AttachmentSource.FromStream(stream));
            return this;
        }

        public EmailLoggerConfig AddAttachment(byte[] bytes)
        {
            Attachments.Add(AttachmentSource.FromBytes(bytes));
            return this;
        }
    }

    internal class AttachmentSource
    {
        public string? FilePath { get; set; }
        public Stream? FileStream { get; set; }
        public byte[]? FileBytes { get; set; }

        public static AttachmentSource FromFilePath(string path) => new AttachmentSource { FilePath = path };
        public static AttachmentSource FromStream(Stream stream) => new AttachmentSource { FileStream = stream };
        public static AttachmentSource FromBytes(byte[] bytes) => new AttachmentSource { FileBytes = bytes };
    }


}
