using System;
using System.Collections.Generic;

namespace cAmp.Libraries.Common.Objects
{
    public class Album
    {
        public Album()
        {
            Songs = new List<SoundFile>();
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public Artist Artist { get; set; }

        public List<SoundFile> Songs { get; set; }
    }
}
