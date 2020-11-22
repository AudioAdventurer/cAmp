using System;
using LiteDB;

namespace cAmp.Libraries.Common.Interfaces
{
    public interface IcAmpObject
    {
        [BsonId]
        Guid Id { get; set; }
    }
}
