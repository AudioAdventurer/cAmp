using System;
using cAmp.Libraries.Common.Interfaces;

namespace cAmp.Libraries.Common.Objects
{
    public class PlayListSoundFile : IcAmpObject
    {
        public PlayListSoundFile()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public Guid PlayListId { get; set; }

        public string Filename { get; set; }
    }
}
