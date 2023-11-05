namespace SwiftLogger.Configs
{
    public class FileLoggerConfig : BaseLoggerConfig<FileLoggerConfig>
    {
        internal string FilePath { get; private set; } = "log.txt";

        internal long? MaxFileSizeInBytes { get; private set; } // null indicates no size limit
        internal bool SeparateByDate { get; private set; } = false;

        public FileLoggerConfig SetFilePath(string path)
        {
            FilePath = path;
            return this;
        }


        public FileLoggerConfig SetMaxFileSize(long sizeInBytes)
        {
            MaxFileSizeInBytes = sizeInBytes;
            return this;
        }

        public FileLoggerConfig EnableSeparationByDate()
        {
            SeparateByDate = true;
            return this;
        }

        // Any additional methods or properties specific to FileLoggerConfig can be added here.
    }
}
