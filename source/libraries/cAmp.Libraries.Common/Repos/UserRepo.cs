using System;
using System.Collections.Generic;
using System.Text;
using cAmp.Libraries.Common.Objects;
using cAmp.Libraries.Common.Records;
using LiteDB;

namespace cAmp.Libraries.Common.Repos
{
    public class UserRepo : AbstractRepo<User>
    {
        public UserRepo(LiteDatabase db) 
            : base(db)
        {
        }

        public override string CollectionName => "Users";

        public User GetByUsername(string username)
        {
            var q = Query.EQ("Username", username);
            return Collection.FindOne(q);
        }
    }
}
