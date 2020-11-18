using System.Collections.Generic;
using cAmp.Libraries.Common.Interfaces;

namespace cAmp.Libraries.Common.Objects
{
    public class Album : AbstractcAmpObject
    {
        public Album()
        {
            Songs = new List<SoundFile>();
        }

        public string Name { get; set; }

        public Artist Artist { get; set; }

        public List<SoundFile> Songs { get; set; }
        public override IcAmpObject ToUserInterfaceObject()
        {
            return new UserInterfaceObjects.Album
            {
                Id = Id,
                Artist = Artist?.Name,
                Name = Name
            };
        }
    }
}
