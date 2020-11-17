using System;
using System.Collections.Generic;
using System.Text;

namespace cAmp.Libraries.Common.Objects
{
    public class Artist
    {
        public Artist()
        {
            Albums = new List<Album>();
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<Album> Albums { get; set; }
    }
}
