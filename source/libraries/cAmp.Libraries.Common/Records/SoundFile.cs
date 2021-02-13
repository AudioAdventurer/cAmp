using System.Collections.Generic;
using cAmp.Libraries.Common.Interfaces;

namespace cAmp.Libraries.Common.Records
{
    public record SoundFile : AbstractcAmpRecord
    {
        public SoundFile()
        {
            Genre = new List<string>();
        }

        public string Title { get; init; }
        public string Filename { get; init; }
        public List<string> Genre { get; init; }
        public uint TrackNumber { get; init; }
        public uint Year { get; init; }
        
        public Artist Artist { get; init; }
        public Album Album { get; init; }
        
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
