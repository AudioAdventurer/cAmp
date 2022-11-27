using System.Collections.Generic;

namespace cAmp.Utility.Renamer.Objects
{
    public class ChangeFile
    {
        public ChangeFile()
        {
            Changes = new List<Change>();
        }

        public string Source { get; set; }
        public string Destination { get; set; }
        public List<Change> Changes { get; set; }
    }
}
