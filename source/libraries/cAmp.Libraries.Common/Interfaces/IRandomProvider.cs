using System.Collections.Generic;
using cAmp.Libraries.Common.Objects;

namespace cAmp.Libraries.Common.Interfaces
{
    public interface IRandomProvider
    {
        public List<SoundFile> CreateRandomList(
            List<SoundFile> source,
            int outputSize);

        public string Name { get; }

        public string Description { get; }
    }
}
