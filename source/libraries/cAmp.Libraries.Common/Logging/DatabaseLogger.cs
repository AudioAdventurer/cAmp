using System;
using cAmp.Libraries.Common.Interfaces;
using cAmp.Libraries.Common.Records;
using cAmp.Libraries.Common.Repos;

namespace cAmp.Libraries.Common.Logging
{
    public class DatabaseLogger : IcAmpLogger
    {
        private readonly LogEntryRepo _logEntryRepo;

        public DatabaseLogger(LogEntryRepo logEntryRepo)
        {
            _logEntryRepo = logEntryRepo;
        }

        private void SaveEntry(
            string level,
            string message)
        {
            var entry = new LogEntry
            {
                Time = DateTime.UtcNow,
                Level = level,
                Message = message
            };

            _logEntryRepo.Save(entry);
        }

        public void Verbose(string message)
        {
            SaveEntry("Verbose", message);
        }

        public void Debug(string message)
        {
            SaveEntry("Debug", message);
        }

        public void Info(string message)
        {
            SaveEntry("Info", message);
        }

        public void Warning(string message)
        {
            SaveEntry("Warning", message);
        }

        public void Error(string message)
        {
            SaveEntry("Error", message);
        }

        public void Error(Exception ex)
        {
            SaveEntry("Error", ex.Message);
        }

        public void Fatal(string message)
        {
            SaveEntry("Fatal", message);
        }
    }
}
