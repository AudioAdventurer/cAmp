namespace cAmp.Libraries.Common.Objects
{
    public class TagInfo
    {
        public TagInfo()
        {
            HasTag = true;
        }

        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string[] Genre { get; set; }
        public uint TrackNumber { get; set; }
        public uint Year { get; set; }
        public bool HasTag { get; set; }
    }
}
