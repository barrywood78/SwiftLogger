using SwiftLogger;
using SwiftLogger.Configs;
using SwiftLogger.Enums;
using SwiftLogger.Models;

var consoleConfig = new ConsoleLoggerConfig()
    .SetColorForLogLevel(LogLevel.Error, ConsoleColor.Cyan) 
    .SetColorForLogLevel(LogLevel.Warning, ConsoleColor.Green) 
    .SetMinimumLogLevel(LogLevel.Information);

var fileConfig = new FileLoggerConfig()
    .SetExcludeLogLevel(LogLevel.Error)
    .SetMinimumLogLevel(LogLevel.Information)
    .SetFilePath(@"C:\Logs\SwiftLogger.txt")
    .EnableSeparationByDate();


string? smtpPassword = Environment.GetEnvironmentVariable("LOGGER_EMAIL_PASSWORD", EnvironmentVariableTarget.User) ?? string.Empty;
string? emailFrom = Environment.GetEnvironmentVariable("LOGGER_EMAIL_FROM", EnvironmentVariableTarget.User) ?? string.Empty;
string? emailToSms = Environment.GetEnvironmentVariable("LOGGER_EMAIL_TO_SMS", EnvironmentVariableTarget.User) ?? string.Empty;

var emailConfig = new EmailLoggerConfig()
    .SetSmtpServer("smtp.gmail.com")
    .SetSmtpPort(587)
    .UseSecureSocketLayer(true)
    .SetAuthentication(emailFrom, smtpPassword)
    .SetFromAddress(emailFrom)
    .AddTo(emailToSms) // emailToSms = Email address that sends SMS (text message)
    .SetMessageTemplate("Critical Log: {Timestamp} - {Message}")
    .SetMinimumLogLevel(LogLevel.Critical) // Only send emails for Critical Logs
    .SetSubjectFormat("App Log");

// Build the logger using configuration
var logger = new LoggerConfigBuilder()
    .LogTo.Console(consoleConfig)
    .LogTo.File(fileConfig)
    .LogTo.Email(emailConfig)
    .Build();


await logger.Log(LogLevel.Trace, "This is a Trace message.");
await logger.Log(LogLevel.Debug, "This is a debug message.");
await logger.Log(LogLevel.Information, "This is an informational message.");
await logger.Log(LogLevel.Warning, "This is a warning message.");
await logger.Log(LogLevel.Error, "This is an error message.");

emailConfig.ClearAttachments().AddAttachment(@"C:\Untitled.png"); // Clears all set attachments and then adds new one
await logger.Log(LogLevel.Critical, "This is a critical message.");

Console.WriteLine("Press any key to exit...");
Console.ReadKey();
