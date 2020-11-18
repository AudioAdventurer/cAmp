using System;
using System.Collections.Generic;
using cAmp.Libraries.Common.Interfaces;
using TagLib.Riff;

namespace cAmp.Libraries.Common.UserInterfaceObjects
{
    public class SoundFile : IcAmpObject
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public uint TrackNumber { get; set; }

        public List<string> Genre { get; set; }

    }
}
