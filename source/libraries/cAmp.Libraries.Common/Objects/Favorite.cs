using System;

namespace cAmp.Libraries.Common.Objects
{
    public class Favorite
    {
        public Guid UserId { get; set; }
        public string SoundFile { get; set; }
        public DateTime TimeSet { get; set; }
    }
}
