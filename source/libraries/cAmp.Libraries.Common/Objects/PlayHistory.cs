using System;
using cAmp.Libraries.Common.Interfaces;

namespace cAmp.Libraries.Common.Objects
{
    public class PlayHistory : IcAmpObject
    {
        public PlayHistory()
        {
            Id = Guid.NewGuid();
        }

        public string Filename { get; set; }

        public DateTime Ended { get; set; }

        public bool PlayedToEnd { get; set; }

        public Guid Id { get; set; }

        public Guid UserId { get; set; }
    }
}
