using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using SwiftLogger.Enums;

namespace SwiftLogger.Configs
{
    public sealed class EmailLoggerConfig : BaseLoggerConfig<EmailLoggerConfig>
    {
        public string SmtpServer { get; private set; }
        public int SmtpPort { get; private set; }
        public bool UseSsl { get; private set; }
        public NetworkCredential Credentials { get; private set; }
        public string FromAddress { get; private set; }
        public string SubjectFormat { get; private set; }
        private List<MailAddress> ToRecipients { get; } = new List<MailAddress>();
        private List<MailAddress> CcRecipients { get; } = new List<MailAddress>();
        private List<MailAddress> BccRecipients { get; } = new List<MailAddress>();
        private List<Attachment> Attachments { get; } = new List<Attachment>();
        private List<string> _attachmentPaths = new List<string>();


        public EmailLoggerConfig SetSmtpServer(string server)
        {
            SmtpServer = server;
            return this;
        }

        public EmailLoggerConfig SetSmtpPort(int port)
        {
            SmtpPort = port;
            return this;
        }

        public EmailLoggerConfig UseSecureSocketLayer(bool useSsl)
        {
            UseSsl = useSsl;
            return this;
        }

        public EmailLoggerConfig SetAuthentication(string username, string password)
        {
            Credentials = new NetworkCredential(username, password);
            return this;
        }

        public EmailLoggerConfig SetFromAddress(string fromAddress)
        {
            FromAddress = fromAddress;
            return this;
        }

        public EmailLoggerConfig SetSubjectFormat(string subjectFormat)
        {
            SubjectFormat = subjectFormat;
            return this;
        }

        public EmailLoggerConfig AddTo(string toAddress)
        {
            ToRecipients.Add(new MailAddress(toAddress));
            return this;
        }

        public EmailLoggerConfig AddCc(string ccAddress)
        {
            CcRecipients.Add(new MailAddress(ccAddress));
            return this;
        }

        public EmailLoggerConfig AddBcc(string bccAddress)
        {
            BccRecipients.Add(new MailAddress(bccAddress));
            return this;
        }

        public EmailLoggerConfig AddAttachment(string filePath)
        {
            _attachmentPaths.Add(filePath);
            return this;
        }

        public IEnumerable<string> GetAttachmentPaths()
        {
            return _attachmentPaths.AsEnumerable();
        }

        public EmailLoggerConfig ClearAttachments()
        {
            _attachmentPaths.Clear();
            return this;
        }

        internal IEnumerable<MailAddress> GetToRecipients() => ToRecipients.AsReadOnly();
        internal IEnumerable<MailAddress> GetCcRecipients() => CcRecipients.AsReadOnly();
        internal IEnumerable<MailAddress> GetBccRecipients() => BccRecipients.AsReadOnly();
        internal IEnumerable<Attachment> GetCurrentAttachments() => Attachments.AsReadOnly();
    }
}
