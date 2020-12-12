using System;
using System.Collections.Generic;
using System.Linq;
using cAmp.Libraries.Common.Objects;
using LiteDB;

namespace cAmp.Libraries.Common.Repos
{
    public class PlayListRepo : AbstractRepo<PlayList>
    {
        public PlayListRepo(LiteDatabase db) 
            : base(db)
        {
            Collection.EnsureIndex("OwnerUserId", false);
        }

        public override string CollectionName => "PlayLists";

        public IEnumerable<PlayList> GetByUser(Guid userId)
        {
            return GetItems(Query.EQ("OwnerUserId", userId));
        }

        public IEnumerable<PlayList> GetSharedByOthers(Guid userId)
        {
            var items = GetItems(Query.EQ("IsShared", true));

            List<PlayList> shared = new List<PlayList>();

            foreach (PlayList playList in items)
            {
                if (playList.OwnerUserId != userId)
                {
                    shared.Add(playList);
                }
            }

            return shared;
        }

        public PlayList GetFavorites(Guid userId)
        {
            var playList = Collection.FindOne(
                Query.And(
                    Query.EQ("OwnerUserId", userId), 
                    Query.EQ("Name", "Favorites")));
            return playList;
        }

        public PlayList EnsureFavoritesPlayList(Guid userId)
        {
            var favorites = GetFavorites(userId);

            if (favorites == null)
            {
                favorites = new PlayList
                {
                    Name = "Favorites",
                    Description = "Built in list of favorite songs", 
                    OwnerUserId = userId
                };

                Save(favorites);
            }

            return favorites;
        }
    }
}
