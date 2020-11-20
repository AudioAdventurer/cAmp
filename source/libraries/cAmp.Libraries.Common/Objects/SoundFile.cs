using System.Collections.Generic;
using cAmp.Libraries.Common.Interfaces;

namespace cAmp.Libraries.Common.Objects
{
    public class SoundFile : AbstractcAmpObject
    {
        public SoundFile()
        {
            Genre = new List<string>();
        }

        public string Title { get; set; }
        public string Filename { get; set; }
        public List<string> Genre { get; set; }
        public uint TrackNumber { get; set; }
        
        public Artist Artist { get; set; }
        public Album Album { get; set; }
        
        public override IcAmpObject ToUserInterfaceObject()
        {
            return new UserInterfaceObjects.SoundFile
            {
                Id = Id,
                Title = Title,
                TrackNumber =  TrackNumber,
                Genre = Genre,
                Artist = Artist?.Name,
                ArtistId = Artist?.Id,
                Album = Album?.Name,
                AlbumId = Album?.Id
            };
        }
    }
}
