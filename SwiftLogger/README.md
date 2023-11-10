# SwiftLogger

## Introduction
SwiftLogger is a versatile logging library for .NET applications, providing a range of log levels from Trace to Critical, along with configurable logging targets such as console, file, and email.

## Features
- Detailed logging with multiple levels (Trace, Debug, Information, Warning, Error, Critical, None)
- Customizable log formats and timestamp formats
- Console, File, and Email logging with configurable options
- Log level exclusion and minimum log level settings
- Support for log separation by date (File logger)
- Email logging with attachment support

## Requirements
- .NET version 7

## Configuration
Each logger can be fully customized. Here are some examples of how to configure them:

### Console Logger Configuration
```csharp
var consoleConfig = new ConsoleLoggerConfig()
    .SetColorForLogLevel(LogLevel.Error, ConsoleColor.Cyan) 
    .SetColorForLogLevel(LogLevel.Warning, ConsoleColor.Green) 
    .SetMinimumLogLevel(LogLevel.Information);
```

### File Logger Configuration
```csharp
var fileConfig = new FileLoggerConfig()
    .SetExcludeLogLevel(LogLevel.Error)
    .SetMinimumLogLevel(LogLevel.Information)
    .SetFilePath(@"C:\Logs\SwiftLogger.txt")
    .EnableSeparationByDate();
```

### Email Logger Configuration
```csharp
// Ensure environment variables are set for email credentials
var emailConfig = new EmailLoggerConfig()
    .SetSmtpServer("smtp.gmail.com")
    .SetSmtpPort(587)
    .UseSecureSocketLayer(true)
    .SetAuthentication("your_email@gmail.com", smtpPassword) // Use your SMTP credentials
    .SetFromAddress("your_email@gmail.com")
    .AddTo("recipient_sms@example.com") // Can be an SMS gateway email
    .SetMessageTemplate("Critical Log: {Timestamp} - {Message}")
    .SetMinimumLogLevel(LogLevel.Critical) // Only sends emails for Critical logs
    .SetSubjectFormat("App Log");
```

### Note on Default Configurations
SwiftLogger is designed to be flexible and can work with default settings out of the box for the Console and File loggers. You can quickly start without providing any configuration:

```csharp
var logger = new LoggerConfigBuilder()
    .LogTo.Console()
    .LogTo.File()
    .Build();
```

This will use the Console and File loggers with their default configurations. The Email logger, due to the nature of email sending, requires SMTP setup details to be specified.

## Getting Started
This section will guide you through setting up SwiftLogger with both customized and default configurations:

```csharp
// For customized logging
var logger = new LoggerConfigBuilder()
    .LogTo.Console(consoleConfig)
    .LogTo.File(fileConfig)
    .LogTo.Email(emailConfig) // Email logger requires configuration for SMTP details
    .Build();

// For default logging (excluding EmailLogger)
var defaultLogger = new LoggerConfigBuilder()
    .LogTo.Console()
    .LogTo.File()
    .Build();

// Log messages of varying severity
await logger.Log(LogLevel.Trace, "This is a Trace message.");
await logger.Log(LogLevel.Debug, "This is a debug message.");
await logger.Log(LogLevel.Information, "This is an informational message.");
await logger.Log(LogLevel.Warning, "This is a warning message.");
await logger.Log(LogLevel.Error, "This is an error message.");

// For critical logs with the customized logger, clear previous attachments and add a new one, then send the log
emailConfig.ClearAttachments().AddAttachment(@"C:\Path\To\Attachment.png");
await logger.Log(LogLevel.Critical, "This is a critical message.");
```

## Configuration Options and Placeholders

Each logger configuration inherits a set of common options from the `BaseLoggerConfig<T>` class, which apply across all loggers, with default values provided for convenience.

### Common Configuration Options (Defaults where applicable)
- `SetTimestampFormat(string format)`: Defines the format of the timestamp in log messages. Default is `"yyyy-MM-dd HH:mm:ss"`.
- `SetMessageTemplate(string template)`: Sets the template for the log message. Default is `"{Timestamp} [{Level}] {Message}"`. Placeholders `{Timestamp}`, `{Level}`, and `{Message}` should be used within this template.
- `SetMinimumLogLevel(LogLevel level)`: Establishes the minimum log level required for a log message to be processed. Without setting this, all levels are logged.
- `DisableLogging()`: Disables logging when called. By default, logging is enabled.
- `SetExcludeLogLevel(LogLevel level)`: Excludes specific log levels from being logged. No levels are excluded by default.

### ConsoleLoggerConfig
Inherits all common options and adds:
- `SetColorForLogLevel(LogLevel level, ConsoleColor color)`: Sets the color for a specific log level in console output. Default colors are:
  - `Trace`: `ConsoleColor.White`
  - `Debug`: `ConsoleColor.White`
  - `Information`: `ConsoleColor.White`
  - `Warning`: `ConsoleColor.Yellow`
  - `Error`: `ConsoleColor.Red`
  - `Critical`: `ConsoleColor.Red`

### FileLoggerConfig
Inherits all common options and adds:
- `SetFilePath(string path)`: Sets the path to the log file. The default is `"log.txt"`.
- `SetMaxFileSize(long sizeInBytes)`: Sets the maximum file size for the log file before it rolls over to a new file. There is no default size limit, meaning rollover is not enabled by default.
- `EnableSeparationByDate()`: Configures the logger to separate log files by date. This is not enabled by default.

### EmailLoggerConfig
Inherits all common options and requires additional configuration due to the nature of email:
- `SetSmtpServer(string server)`, `SetSmtpPort(int port)`: Must be set to the SMTP server details.
- `UseSecureSocketLayer(bool useSsl)`: Specifies whether to use SSL. Must be set according to the SMTP server requirements.
- `SetAuthentication(string username, string password)`: Sets the credentials for the SMTP server.
- `SetFromAddress(string fromAddress)`: Sets the 'From' email address for log emails.
- `SetSubjectFormat(string subjectFormat)`: Defines the subject format for email logs using placeholders. This is optional and has no default value.
- `AddTo(string toAddress)`, `AddCc(string ccAddress)`, `AddBcc(string bccAddress)`: Adds respective recipients to the email. At least one 'To' address is required.
- `AddAttachment(string filePath)`: Adds an attachment to the email. This is optional and has no attachments by default.
- `ClearAttachments()`: Clears all attachments from the email configuration.

Proper use of placeholders in `SetMessageTemplate` and `SetSubjectFormat` is essential for including dynamic content such as timestamps, log levels, and messages in your logs.


---



## API Reference

The API Reference section details all the public classes, methods, and properties available in SwiftLogger, providing you with the necessary information to utilize the logger to its full potential.

### SwiftLogger

The main logging class responsible for managing and dispatching log messages to the configured loggers.

#### Methods

- `Log(LogLevel level, string message)`: Asynchronously logs a message with the specified log level.

### LoggerConfigBuilder

A class used to construct a `SwiftLogger` instance with a fluent API for configuration.

#### Methods

- `LogTo`: Accessor for configuring individual loggers such as Console, File, and Email.
- `Build()`: Finalizes the configuration and builds the `SwiftLogger` instance.

### LoggerProviders

Used within `LoggerConfigBuilder` to add and configure specific logger providers.

#### Methods

- `Console(ConsoleLoggerConfig? config = null)`: Adds a console logger with optional configuration.
- `File(FileLoggerConfig? config = null)`: Adds a file logger with optional configuration.
- `Email(EmailLoggerConfig? config = null)`: Adds an email logger with required configuration.

### ConsoleLoggerConfig

Configures logging to the console.

#### Methods

- Inherits common configuration methods from `BaseLoggerConfig<T>`.
- `SetColorForLogLevel(LogLevel level, ConsoleColor color)`: Sets the color for a specific log level.

### FileLoggerConfig

Configures logging to a file.

#### Methods

- Inherits common configuration methods from `BaseLoggerConfig<T>`.
- `SetFilePath(string path)`: Sets the path for the log file.
- `SetMaxFileSize(long sizeInBytes)`: Sets the maximum size of the log file before rolling over.
- `EnableSeparationByDate()`: Separates log files by date.

### EmailLoggerConfig

Configures logging to send logs via email.

#### Methods

- Inherits common configuration methods from `BaseLoggerConfig<T>`.
- `SetSmtpServer(string server)`: Sets the SMTP server address.
- `SetSmtpPort(int port)`: Sets the SMTP port.
- `UseSecureSocketLayer(bool useSsl)`: Configures SSL usage for email.
- `SetAuthentication(string username, string password)`: Sets SMTP authentication credentials.
- `SetFromAddress(string fromAddress)`: Sets the 'From' email address.
- `SetSubjectFormat(string subjectFormat)`: Sets the subject line format for email logs.
- `AddTo(string toAddress)`: Adds a recipient to the email.
- `AddCc(string ccAddress)`: Adds a CC recipient to the email.
- `AddBcc(string bccAddress)`: Adds a BCC recipient to the email.
- `AddAttachment(string filePath)`: Adds an attachment to the email.
- `ClearAttachments()`: Clears all attachments from the email.

The placeholders `{Timestamp}`, `{Level}`, and `{Message}` can be used within `SetMessageTemplate` for `BaseLoggerConfig<T>` and `SetSubjectFormat` for `EmailLoggerConfig` to dynamically include log event details.



## License
The SwiftLogger is released under MIT License


---

