using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftLogger.Enums
{
    public enum LogLevel
    {
        // Very detailed logs, typically used for diagnosing problems.
        // Might be too verbose for everyday logging.
        Trace = 1,

        // Useful for information during development or troubleshooting.
        // More detailed than 'Information' but not as verbose as 'Trace'.
        Debug = 2,

        // General system operational logs. E.g., starting/stopping a service, user logins.
        // Doesn't contain the level of detail found in 'Debug' or 'Trace'.
        Information = 3,

        // Indicates non-critical issues that might not interrupt the application flow.
        // Situations that administrators or developers should be aware of.
        Warning = 4,

        // Represents errors that may interrupt the application's flow.
        // Not critical enough to terminate the system but indicates issues.
        Error = 5,

        // Severe failures like data loss, system outages, or other catastrophic failures.
        // Might require immediate attention and intervention.
        Critical = 6,

        // Used to completely disable logging, useful in performance-critical scenarios.
        // Eliminates even the overhead of checking the log level.
        None = 7
    }

}
