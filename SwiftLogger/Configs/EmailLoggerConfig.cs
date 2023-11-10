using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using SwiftLogger.Enums;

namespace SwiftLogger.Configs
{
    /// <summary>
    /// Represents a configuration for email logging.
    /// Extends the <see cref="BaseLoggerConfig{T}"/> to configure email-specific settings,
    /// such as SMTP server details, authentication, and email composition.
    /// </summary>
    public sealed class EmailLoggerConfig : BaseLoggerConfig<EmailLoggerConfig>
    {
        /// <summary>
        /// The SMTP server used for sending emails.
        /// </summary>
        public string? SmtpServer { get; private set; }

        /// <summary>
        /// The port used for the SMTP server.
        /// </summary>
        public int SmtpPort { get; private set; }

        /// <summary>
        /// Indicates whether SSL should be used for the SMTP connection.
        /// </summary>
        public bool UseSsl { get; private set; }

        /// <summary>
        /// Credentials used for authenticating with the SMTP server.
        /// </summary>
        public NetworkCredential? Credentials { get; private set; }

        /// <summary>
        /// The email address from which log emails are sent.
        /// </summary>
        public string? FromAddress { get; private set; }

        /// <summary>
        /// The format for the subject line of the log emails.
        /// </summary>
        public string? SubjectFormat { get; private set; }

        // Internal lists for managing email recipients and attachments.
        private List<MailAddress> ToRecipients { get; } = new List<MailAddress>();
        private List<MailAddress> CcRecipients { get; } = new List<MailAddress>();
        private List<MailAddress> BccRecipients { get; } = new List<MailAddress>();
        private List<Attachment> Attachments { get; } = new List<Attachment>();
        private List<string> _attachmentPaths = new();

        /// <summary>
        /// Sets the SMTP server for email sending.
        /// </summary>
        /// <param name="server">The SMTP server address.</param>
        /// <returns>The email logger configuration instance.</returns>
        public EmailLoggerConfig SetSmtpServer(string server)
        {
            SmtpServer = server;
            return this;
        }

        /// <summary>
        /// Sets the port for the SMTP server.
        /// </summary>
        /// <param name="port">The port number.</param>
        /// <returns>The email logger configuration instance.</returns>
        public EmailLoggerConfig SetSmtpPort(int port)
        {
            SmtpPort = port;
            return this;
        }

        /// <summary>
        /// Configures the use of SSL for the SMTP connection.
        /// </summary>
        /// <param name="useSsl">Whether to use SSL.</param>
        /// <returns>The email logger configuration instance.</returns>
        public EmailLoggerConfig UseSecureSocketLayer(bool useSsl)
        {
            UseSsl = useSsl;
            return this;
        }

        /// <summary>
        /// Sets the authentication credentials for the SMTP server.
        /// </summary>
        /// <param name="username">The username for authentication.</param>
        /// <param name="password">The password for authentication.</param>
        /// <returns>The email logger configuration instance.</returns>
        public EmailLoggerConfig SetAuthentication(string username, string password)
        {
            Credentials = new NetworkCredential(username, password);
            return this;
        }

        /// <summary>
        /// Sets the email address from which log emails will be sent.
        /// </summary>
        /// <param name="fromAddress">The sender's email address.</param>
        /// <returns>The email logger configuration instance.</returns>
        public EmailLoggerConfig SetFromAddress(string fromAddress)
        {
            FromAddress = fromAddress;
            return this;
        }

        /// <summary>
        /// Sets the format for the subject line of log emails.
        /// </summary>
        /// <param name="subjectFormat">The subject format string.</param>
        /// <returns>The email logger configuration instance.</returns>
        public EmailLoggerConfig SetSubjectFormat(string subjectFormat)
        {
            SubjectFormat = subjectFormat;
            return this;
        }

        /// <summary>
        /// Adds an email address to the list of 'To' recipients.
        /// </summary>
        /// <param name="toAddress">The 'To' email address.</param>
        /// <returns>The email logger configuration instance.</returns>
        public EmailLoggerConfig AddTo(string toAddress)
        {
            ToRecipients.Add(new MailAddress(toAddress));
            return this;
        }

        /// <summary>
        /// Adds an email address to the list of 'CC' recipients.
        /// </summary>
        /// <param name="ccAddress">The 'CC' email address.</param>
        /// <returns>The email logger configuration instance.</returns>
        public EmailLoggerConfig AddCc(string ccAddress)
        {
            CcRecipients.Add(new MailAddress(ccAddress));
            return this;
        }

        /// <summary>
        /// Adds an email address to the list of 'BCC' recipients.
        /// </summary>
        /// <param name="bccAddress">The 'BCC' email address.</param>
        /// <returns>The email logger configuration instance.</returns>
        public EmailLoggerConfig AddBcc(string bccAddress)
        {
            BccRecipients.Add(new MailAddress(bccAddress));
            return this;
        }

        /// <summary>
        /// Adds a file attachment to the email.
        /// </summary>
        /// <param name="filePath">The file path of the attachment.</param>
        /// <returns>The email logger configuration instance.</returns>
        public EmailLoggerConfig AddAttachment(string filePath)
        {
            _attachmentPaths.Add(filePath);
            return this;
        }

        /// <summary>
        /// Retrieves the paths of the currently configured attachments.
        /// </summary>
        /// <returns>An enumerable of file paths.</returns>
        public IEnumerable<string> GetAttachmentPaths()
        {
            return _attachmentPaths.AsEnumerable();
        }

        /// <summary>
        /// Clears all configured file attachments.
        /// </summary>
        /// <returns>The email logger configuration instance.</returns>
        public EmailLoggerConfig ClearAttachments()
        {
            _attachmentPaths.Clear();
            return this;
        }

        // Internal methods to retrieve recipients and current attachments.
        internal IEnumerable<MailAddress> GetToRecipients() => ToRecipients.AsReadOnly();
        internal IEnumerable<MailAddress> GetCcRecipients() => CcRecipients.AsReadOnly();
        internal IEnumerable<MailAddress> GetBccRecipients() => BccRecipients.AsReadOnly();
        internal IEnumerable<Attachment> GetCurrentAttachments() => Attachments.AsReadOnly();
    }
}
