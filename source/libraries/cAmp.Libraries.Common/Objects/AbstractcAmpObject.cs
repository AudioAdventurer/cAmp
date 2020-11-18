using System;
using cAmp.Libraries.Common.Interfaces;

namespace cAmp.Libraries.Common.Objects
{
    public abstract class AbstractcAmpObject : IcAmpObject
    {
        protected AbstractcAmpObject()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public abstract IcAmpObject ToUserInterfaceObject();
    }
}
