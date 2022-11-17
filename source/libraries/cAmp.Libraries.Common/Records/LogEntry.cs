using System;
using cAmp.Libraries.Common.Interfaces;

namespace cAmp.Libraries.Common.Records
{
    public record LogEntry : AbstractcAmpRecord
    {
        public DateTime Time { get; set; }
        
        public string Level { get; set; }

        public string Message { get; set; }

        public override IcAmpObject ToUserInterfaceObject()
        {
            var obj = new UserInterfaceObjects.LogEntry()
            {
                Id = this.Id,
                Level = this.Level,
                Message = this.Message,
                Time = this.Time
            };

            return obj;
        }
    }
}
