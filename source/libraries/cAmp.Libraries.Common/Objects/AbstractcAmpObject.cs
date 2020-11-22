using System;
using cAmp.Libraries.Common.Interfaces;
using LiteDB;

namespace cAmp.Libraries.Common.Objects
{
    public abstract class AbstractcAmpObject : IcAmpObject
    {
        protected AbstractcAmpObject()
        {
            Id = Guid.NewGuid();
        }

        [BsonId]
        public Guid Id { get; set; }

        public abstract IcAmpObject ToUserInterfaceObject();
    }
}
