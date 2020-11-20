using cAmp.Libraries.Common.Interfaces;
using System;

namespace cAmp.Libraries.Common.UserInterfaceObjects
{
    public class Album : IcAmpObject
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Artist { get; set; }

        public Guid? ArtistId { get; set; }
    }
}
