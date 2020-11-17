using System;
using System.Collections.Generic;

namespace cAmp.Libraries.Common.Objects
{
    public class SoundFile
    {
        public SoundFile()
        {
            Id = Guid.NewGuid();
            Genre = new List<string>();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Filename { get; set; }
        public List<string> Genre { get; set; }
        public uint TrackNumber { get; set; }



        public Artist Artist { get; set; }
        public Album Album { get; set; }
    }
}
