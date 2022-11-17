using System;
using cAmp.Libraries.Common.Interfaces;

namespace cAmp.Libraries.Common.UserInterfaceObjects
{
    public class LogEntry : IcAmpObject
    {
        public Guid Id { get; set; }

        public DateTime Time { get; set; }

        public string Level { get; set; }

        public string Message { get; set; }

    }
}
