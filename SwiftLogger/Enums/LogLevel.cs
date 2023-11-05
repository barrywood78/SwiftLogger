namespace SwiftLogger.Enums
{
    /// <summary>
    /// Defines the severity levels of log messages.
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// Very detailed logs, typically used for diagnosing problems.
        /// Might be too verbose for everyday logging.
        /// </summary>
        Trace = 1,

        /// <summary>
        /// Useful for information during development or troubleshooting.
        /// More detailed than 'Information' but not as verbose as 'Trace'.
        /// </summary>
        Debug = 2,

        /// <summary>
        /// General system operational logs. E.g., starting/stopping a service, user logins.
        /// Doesn't contain the level of detail found in 'Debug' or 'Trace'.
        /// </summary>
        Information = 3,

        /// <summary>
        /// Indicates non-critical issues that might not interrupt the application flow.
        /// Situations that administrators or developers should be aware of.
        /// </summary>
        Warning = 4,

        /// <summary>
        /// Represents errors that may interrupt the application's flow.
        /// Not critical enough to terminate the system but indicates issues.
        /// </summary>
        Error = 5,

        /// <summary>
        /// Severe failures like data loss, system outages, or other catastrophic failures.
        /// Might require immediate attention and intervention.
        /// </summary>
        Critical = 6,

        /// <summary>
        /// Used to completely disable logging, useful in performance-critical scenarios.
        /// Eliminates even the overhead of checking the log level.
        /// </summary>
        None = 7
    }
}
