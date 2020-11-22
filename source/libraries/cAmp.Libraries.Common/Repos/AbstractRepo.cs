using System;
using cAmp.Libraries.Common.Interfaces;
using LiteDB;

namespace cAmp.Libraries.Common.Repos
{
    public abstract class AbstractRepo<T> where T:IcAmpObject
    {
        protected readonly ILiteCollection<T> Collection;

        public abstract string CollectionName { get; }

        protected AbstractRepo(LiteDatabase db)
        {
            Collection = db.GetCollection<T>(CollectionName);
        }

        public void Save(T obj)
        {
            if (obj.Id.Equals(Guid.Empty))
            {
                throw new Exception("Guid.Empty used as identifier");
            }

            Collection.Upsert(obj);
        }

        public T GetById(Guid id)
        {
            var obj = Collection.FindById(id);
            return obj;
        }
    }
}
