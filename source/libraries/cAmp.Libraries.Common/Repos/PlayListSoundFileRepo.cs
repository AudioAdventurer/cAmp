using System;
using System.Collections.Generic;
using cAmp.Libraries.Common.Objects;
using LiteDB;

namespace cAmp.Libraries.Common.Repos
{
    public class PlayListSoundFileRepo :AbstractRepo<PlayListSoundFile>
    {
        public PlayListSoundFileRepo(LiteDatabase db) 
            : base(db)
        {
        }

        public override string CollectionName => "PlayListSoundFiles";

        public IEnumerable<PlayListSoundFile> GetByPlayList(Guid playListId)
        {
            return GetItems(Query.EQ("PlayListId", playListId));
        }

        public PlayListSoundFile GetByPlayListFileName(Guid playListId, string filename)
        {
            var plsf = GetItem(
                Query.And(
                    Query.EQ("PlayListId", playListId), 
                    Query.EQ("Filename", filename)));

            return plsf;
        }

        public void DeletePlayList(Guid playListId)
        {
            Collection.DeleteMany(Query.EQ("PlayListId", playListId));
        }
    }
}
