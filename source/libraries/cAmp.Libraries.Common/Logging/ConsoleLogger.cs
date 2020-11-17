using System;
using cAmp.Libraries.Common.Interfaces;

namespace cAmp.Libraries.Common.Logging
{
    public class ConsoleLogger : IcAmpLogger
    {
        public void Verbose(string message)
        {
            WriteLine("Verbose", message);
        }

        public void Debug(string message)
        {
            WriteLine("Debug  ", message);
        }

        public void Info(string message)
        {
            WriteLine("Info   ", message);
        }

        public void Warning(string message)
        {
            WriteLine("Warning", message);
        }

        public void Error(string message)
        {
            WriteLine("Error  ", message);
        }

        public void Error(Exception ex)
        {
            WriteLine("Error  ", ex.Message);
        }

        public void Fatal(string message)
        {
            WriteLine("Fatal  ", message);
        }

        private void WriteLine(string level, string message)
        {
            string now = DateTime.UtcNow.ToString("s");
            System.Diagnostics.Trace.WriteLine($"{now} - {level} - {message}");
        }
    }
}
