using System.Collections.Generic;
using cAmp.Libraries.Common.Interfaces;

namespace cAmp.Libraries.Common.Records
{
    public record Artist : AbstractcAmpRecord
    {
        public Artist()
        {
            Albums = new List<Album>();
        }

        public string Name { get; init; }

        public List<Album> Albums { get; init; }

        public override IcAmpObject ToUserInterfaceObject()
        {
            return new UserInterfaceObjects.Artist
            {
                Id = Id,
                Name = Name
            };
        }
    }
}
