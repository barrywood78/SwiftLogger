using SwiftLogger;
using SwiftLogger.Configs;
using SwiftLogger.Enums;
using SwiftLogger.Models;


string? smtpPassword = Environment.GetEnvironmentVariable("LOGGER_EMAIL_PASSWORD", EnvironmentVariableTarget.User) ?? string.Empty;
string? emailFrom = Environment.GetEnvironmentVariable("LOGGER_EMAIL_FROM", EnvironmentVariableTarget.User) ?? string.Empty;
string? emailToSms = Environment.GetEnvironmentVariable("LOGGER_EMAIL_TO_SMS", EnvironmentVariableTarget.User) ?? string.Empty;

var emailConfig = new EmailLoggerConfig()
    .SetSmtpServer("smtp.gmail.com")
    .SetSmtpPort(587)
    .UseSecureSocketLayer(true)
    .SetAuthentication(emailFrom, smtpPassword)
    .SetFromAddress(emailFrom)

    //.AddRecipient(emailToSms)
    .AddRecipient(emailFrom)

    //.SetTimestampFormat("MM-dd HH:mm:ss") // Tested & Worked
    //.SetMessageTemplate("Custom Msg Test: {Level} - {Message} - {Timestamp} - END MSG") // Tested & Worked
    .SetMinimumLogLevel(LogLevel.Critical) // Tested & Worked
    //.SetExcludeLogLevel(LogLevel.Error) // Tested & Worked
    //.DisableLogging() // Tested & Worked

    //.SetSubjectFormat("Test App Log - {Timestamp}: {Level}") // Tested & Worked
    //.AddAttachment(@"C:\Untitled.png") // Tested & Worked
    ;



// Build the logger using configuration
var logger = new LoggerConfigBuilder()
    .LogTo.Email(emailConfig)
    .Build();


await logger.Log(LogLevel.Trace, "This is a Trace message.");
await logger.Log(LogLevel.Debug, "This is a debug message.");
await logger.Log(LogLevel.Information, "This is an informational message.");
await logger.Log(LogLevel.Warning, "This is a warning message.");
await logger.Log(LogLevel.Error, "This is an error message.");
await logger.Log(LogLevel.Critical, "This is a critical message.");

Console.WriteLine("Press any key to exit...");
Console.ReadKey();
