using System.Collections.Generic;
using cAmp.Libraries.Common.Interfaces;

namespace cAmp.Libraries.Common.Objects
{
    public class Artist : AbstractcAmpObject
    {
        public Artist()
        {
            Albums = new List<Album>();
        }

        public string Name { get; set; }

        public List<Album> Albums { get; set; }
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
