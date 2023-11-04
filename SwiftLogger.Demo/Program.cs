using SwiftLogger;
using SwiftLogger.Enums;
using SwiftLogger.Models;

//var consoleConfig = new ConsoleLoggerConfig()
//    .SetColorForLogLevel(LogLevel.Error, ConsoleColor.Blue)
//    .SetColorForLogLevel(LogLevel.Warning, ConsoleColor.Magenta);

var logger = new SwiftLoggerConfig()
                .LogTo.Console()
                .Build();

logger.Log(LogLevel.Debug, "This is a debug message.");
logger.Log(LogLevel.Information, "This is an informational message.");
logger.Log(LogLevel.Warning, "This is a warning message.");
logger.Log(LogLevel.Error, "This is an error message.");
logger.Log(LogLevel.Critical, "This is a critical message.");

Console.WriteLine("Press any key to exit...");
Console.ReadKey();
