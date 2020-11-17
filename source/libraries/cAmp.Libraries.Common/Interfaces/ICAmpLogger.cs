using System;

namespace cAmp.Libraries.Common.Interfaces
{
    public interface IcAmpLogger
    {
        void Verbose(string message);
        void Debug(string message);
        void Info(string message);
        void Warning(string message);
        void Error(string message);
        void Error(Exception ex);
        void Fatal(string message);
    }
}
