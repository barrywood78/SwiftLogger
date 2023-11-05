using SwiftLogger;
using SwiftLogger.Configs;
using SwiftLogger.Enums;
using SwiftLogger.Models;

//var consoleConfig = new ConsoleLoggerConfig()
//    .SetColorForLogLevel(LogLevel.Error, ConsoleColor.Blue)
//    .SetColorForLogLevel(LogLevel.Warning, ConsoleColor.Magenta);

//var logger = new SwiftLoggerConfig()
//                .LogTo.Console()
//                .Build();


//var consoleConfig = new ConsoleLoggerConfig()
//    .SetTimestampFormat("dd/MM/yyyy HH:mm:ss")
//    .SetMessageTemplate("{Timestamp} - {Level}: {Message}")
//    .SetColorForLogLevel(LogLevel.Error, ConsoleColor.Blue)
//    .SetColorForLogLevel(LogLevel.Warning, ConsoleColor.Magenta)
//    .SetExcludeLogLevel(LogLevel.Debug);


//var logger = new SwiftLoggerConfig()
//                .LogTo.Console(consoleConfig)
//                .Build();

// Set up the Console Logger configuration
var consoleConfig = new ConsoleLoggerConfig()
    .SetColorForLogLevel(LogLevel.Error, ConsoleColor.Blue)
    .SetColorForLogLevel(LogLevel.Warning, ConsoleColor.Magenta)
    .SetExcludeLogLevel(LogLevel.Debug)
    .SetTimestampFormat("dd/MM/yyyy HH:mm:ss")
    .SetMessageTemplate("{Timestamp} - {Level}: {Message}");

// Set up the File Logger configuration, with separation by date
var fileLoggerConfig = new FileLoggerConfig()
    .EnableSeparationByDate()
    .SetFilePath("myLog.txt")
    .SetExcludeLogLevel(LogLevel.Debug)
    .SetTimestampFormat("dd/MM/yyyy HH:mm:ss")
    .SetMessageTemplate("{Timestamp} - {Level}: {Message}");  

// Build the logger using both configurations
var logger = new SwiftLoggerConfig()
    .LogTo.Console(consoleConfig)
    .LogTo.File(fileLoggerConfig)
    .Build();



await logger.Log(LogLevel.Debug, "This is a debug message.");
await logger.Log(LogLevel.Information, "This is an informational message.");
await logger.Log(LogLevel.Warning, "This is a warning message.");
await logger.Log(LogLevel.Error, "This is an error message.");
await logger.Log(LogLevel.Critical, "This is a critical message.");

Console.WriteLine("Press any key to exit...");
Console.ReadKey();
