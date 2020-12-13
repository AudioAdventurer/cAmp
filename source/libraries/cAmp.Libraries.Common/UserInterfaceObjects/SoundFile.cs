using System;
using System.Collections.Generic;
using cAmp.Libraries.Common.Interfaces;

namespace cAmp.Libraries.Common.UserInterfaceObjects
{
    public class SoundFile : IcAmpObject
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public uint TrackNumber { get; set; }

        public uint Year { get; set; }

        public bool IsFavorite { get; set; }

        public List<string> Genre { get; set; }

        public string Artist { get; set; }
        public Guid? ArtistId { get; set; }

        public string Album { get; set; }
        public Guid? AlbumId { get; set; }
    }
}
