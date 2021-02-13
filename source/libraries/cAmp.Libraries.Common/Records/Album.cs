using System.Collections.Generic;
using cAmp.Libraries.Common.Interfaces;

namespace cAmp.Libraries.Common.Records
{
    public record Album : AbstractcAmpRecord
    {
        public Album()
        {
            Songs = new List<SoundFile>();
        }

        public string Name { get; init; }

        public Artist Artist { get; init; }

        public List<SoundFile> Songs { get; init; }

        public override IcAmpObject ToUserInterfaceObject()
        {
            return new UserInterfaceObjects.Album
            {
                Id = Id,
                Artist = Artist?.Name,
                Name = Name,
                ArtistId = Artist?.Id
            };
        }
    }
}
