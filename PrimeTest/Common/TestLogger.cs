using System.Text;

namespace PrimeTest.Common
{
    public static class TestLogger
    {
        private static readonly string LogFilePath = "TestResults.log";
        private static readonly object _lock = new();

        public static void Log(
            string testName,
            string testType, // Unit / Integration
            bool passed,
            string? errorMessage = null,
            long durationMs = 0)
        {
            var status = passed ? "✅ PASSED" : "❌ FAILED";

            var log = new StringBuilder();

            log.AppendLine(new string('=', 90));
            log.AppendLine($"Type      : {testType}");
            log.AppendLine($"Timestamp : {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            log.AppendLine($"Test Case : {testName}");
            log.AppendLine($"Status    : {status}");
            log.AppendLine($"Duration  : {durationMs} ms");

            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                log.AppendLine($"Error     : {errorMessage}");
            }

            log.AppendLine(new string('=', 90));
            log.AppendLine();

            lock (_lock)
            {
                File.AppendAllText(LogFilePath, log.ToString());
            }

            Console.WriteLine(log.ToString());
        }
    }
}