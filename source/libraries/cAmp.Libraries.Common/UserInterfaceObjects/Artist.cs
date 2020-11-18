using System;
using cAmp.Libraries.Common.Interfaces;

namespace cAmp.Libraries.Common.UserInterfaceObjects
{
    public class Artist : IcAmpObject
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
