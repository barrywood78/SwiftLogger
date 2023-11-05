using System.Net;
using SwiftLogger.Enums;

namespace SwiftLogger.Configs
{
    public class EmailLoggerConfig : BaseLoggerConfig<EmailLoggerConfig>
    {
        // SMTP server details
        internal string SmtpServer { get; private set; }
        internal int SmtpPort { get; private set; }
        internal bool UseSsl { get; private set; }

        // Authentication details
        internal NetworkCredential Credentials { get; private set; }
        internal bool UseDefaultCredentials { get; private set; } = false;

        // Email details
        internal string FromAddress { get; private set; }
        internal string ToAddress { get; private set; }

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

        public EmailLoggerConfig SetAuthentication(string email, string password)
        {
            Credentials = new NetworkCredential(email, password);
            return this;
        }

        public EmailLoggerConfig UseDefaultCreds(bool useDefault)
        {
            UseDefaultCredentials = useDefault;
            return this;
        }

        public EmailLoggerConfig SetFromAddress(string from)
        {
            FromAddress = from;
            return this;
        }

        public EmailLoggerConfig SetToAddress(string to)
        {
            ToAddress = to;
            return this;
        }

    }
}
