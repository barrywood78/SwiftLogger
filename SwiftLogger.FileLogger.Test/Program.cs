using SwiftLogger;
using SwiftLogger.Configs;
using SwiftLogger.Enums;
using SwiftLogger.Models;

var fileLoggerConfig = new FileLoggerConfig()
    .SetExcludeLogLevel(LogLevel.Error) //Tested & Works
    .SetTimestampFormat("yyyy-MM-dd HH:mm:ss") //Tested & Works
    .SetMessageTemplate("Custom Msg Test: {Level} - {Message} - {Timestamp} - END MSG") //Tested & Works
    .SetMinimumLogLevel(LogLevel.Warning) //Tested & Works
    .SetFilePath(@"C:\Logs\SwiftLogger.txt") //Tested & Works
    .EnableSeparationByDate() //Tested & Works
    .SetMaxFileSize(500) //Tested & Works

    ; 
    //.DisableLogging(); //Tested & Works


// Build the logger using configuration
var logger = new LoggerConfigBuilder()
    .LogTo.File(fileLoggerConfig)
    .Build();


await logger.Log(LogLevel.Trace, "This is a Trace message.");
await logger.Log(LogLevel.Debug, "This is a debug message.");
await logger.Log(LogLevel.Information, "This is an informational message.");
await logger.Log(LogLevel.Warning, "This is a warning message.");
await logger.Log(LogLevel.Error, "This is an error message.");
await logger.Log(LogLevel.Critical, "This is a critical message.");

Console.WriteLine("Press any key to exit...");
Console.ReadKey();
