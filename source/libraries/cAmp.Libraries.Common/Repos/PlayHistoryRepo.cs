using cAmp.Libraries.Common.Objects;
using LiteDB;

namespace cAmp.Libraries.Common.Repos
{
    public class PlayHistoryRepo : AbstractRepo<PlayHistory>
    {
        public PlayHistoryRepo(LiteDatabase db) 
            : base(db)
        {
        }

        public override string CollectionName => "PlayHistory";
    }
}
