using System;
using cAmp.Libraries.Common.Interfaces;
using LiteDB;

namespace cAmp.Libraries.Common.Records
{
    public abstract record AbstractcAmpRecord : IcAmpObject
    {
        protected AbstractcAmpRecord()
        {
            Id = Guid.NewGuid();
        }

        [BsonId]
        public Guid Id { get; init; }

        public abstract IcAmpObject ToUserInterfaceObject();
    }
}
