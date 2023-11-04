using Microsoft.VisualStudio.TestTools.UnitTesting;
using SwiftLogger.Enums;
using System;
using System.IO;

namespace SwiftLogger.Test
{
    [TestClass]
    public class LoggerIntegrationTest
    {
        private StringWriter? _stringWriter;
        private TextWriter? _originalConsoleOut;

        [TestInitialize]
        public void TestSetup()
        {
            // Redirect Console output to the StringWriter instance.
            _originalConsoleOut = Console.Out;
            _stringWriter = new StringWriter();
            Console.SetOut(_stringWriter);
        }

        [TestCleanup]
        public void TestTeardown()
        {
            // Restore the original Console output stream.
            if(_originalConsoleOut !=  null)
            {
                Console.SetOut(_originalConsoleOut);
            }
            
            _stringWriter?.Dispose();
        }

        [TestMethod]
        public void ConsoleLogger_ShouldWriteCorrectlyToConsole()
        {
            // Arrange
            var logger = new SwiftLoggerConfig()
                .LogTo.Console()
                .Build();

            var message = "This is an informational message.";
            var expectedLogOutput = $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} [Information] {message}\r\n"; // Note: \r\n is used for newline in Windows environments.

            // Act
            logger.Log(LogLevel.Information, message);

            // Assert
            var actualLogOutput = _stringWriter?.ToString();
            Assert.AreEqual(expectedLogOutput, actualLogOutput);
        }
    }
}
