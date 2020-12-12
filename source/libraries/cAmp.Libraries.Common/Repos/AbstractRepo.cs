using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<T> GetItems(BsonExpression q, int skip = 0, int limit = Int32.MaxValue)
        {
            var items = Collection.Find(q, skip, limit);
            return items;
        }

        public T GetItem(BsonExpression q)
        {
            var item = Collection.FindOne(q);
            return item;
        }

        public List<T> GetAll()
        {
            return Collection.FindAll()
                .ToList();
        }

        public void Delete(Guid id)
        {
            Collection.Delete(id);
        }
    }
}
