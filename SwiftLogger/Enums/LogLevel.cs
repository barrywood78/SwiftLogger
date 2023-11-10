namespace SwiftLogger.Enums
{
    /// <summary>
    /// Defines the severity levels of log messages. Each level represents a different degree of severity,
    /// allowing for fine-grained control over what gets logged.
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// Very detailed logs, typically used for diagnosing problems.
        /// Might be too verbose for everyday logging but invaluable for pinpointing issues during development or troubleshooting.
        /// </summary>
        Trace = 1,

        /// <summary>
        /// Useful for information during development or troubleshooting.
        /// Provides a more detailed view than 'Information' but is not as verbose as 'Trace'.
        /// Ideal for debugging and gaining insights into the system's behavior.
        /// </summary>
        Debug = 2,

        /// <summary>
        /// General system operational logs, such as service starts/stops, user logins, etc.
        /// Provides a high-level overview of the system's operations without the fine detail found in 'Debug' or 'Trace'.
        /// Suitable for routine logging under normal operations.
        /// </summary>
        Information = 3,

        /// <summary>
        /// Indicates non-critical issues that might not interrupt the application flow.
        /// Useful for alerting administrators or developers to situations that should be monitored or reviewed.
        /// </summary>
        Warning = 4,

        /// <summary>
        /// Represents errors that may interrupt the application's flow.
        /// Indicates significant issues that require attention but are not system-critical.
        /// Essential for reporting runtime errors and exceptions that affect operation.
        /// </summary>
        Error = 5,

        /// <summary>
        /// Severe failures like data loss, system outages, or other catastrophic failures.
        /// Represents critical issues that might require immediate attention and intervention.
        /// Use for the most serious of errors where immediate action is required.
        /// </summary>
        Critical = 6,

        /// <summary>
        /// Used to completely disable logging, useful in performance-critical scenarios.
        /// This level can be employed to eliminate even the overhead of log level checking, thereby optimizing performance.
        /// </summary>
        None = 7
    }
}
