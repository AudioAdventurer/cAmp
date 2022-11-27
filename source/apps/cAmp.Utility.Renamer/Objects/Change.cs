using cAmp.Libraries.Common.Objects;

namespace cAmp.Utility.Renamer.Objects
{
    public class Change
    {
        public string OldFileName { get; set; }
        public TagInfo OldTag { get; set; }
        public string NewFileName { get; set; }
        public TagInfo NewTag { get; set; }
    }
}
