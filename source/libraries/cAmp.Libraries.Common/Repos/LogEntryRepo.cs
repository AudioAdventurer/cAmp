using cAmp.Libraries.Common.Records;
using LiteDB;

namespace cAmp.Libraries.Common.Repos
{
    public class LogEntryRepo : AbstractRepo<LogEntry>
    {
        public LogEntryRepo(LiteDatabase db) : base(db)
        {
        }

        public override string CollectionName => "LogEntries";
    }
}
