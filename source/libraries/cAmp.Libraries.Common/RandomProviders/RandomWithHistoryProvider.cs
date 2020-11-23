using System;
using System.Collections.Generic;
using cAmp.Libraries.Common.Interfaces;
using cAmp.Libraries.Common.Objects;

namespace cAmp.Libraries.Common.RandomProviders
{
    public class RandomWithHistoryProvider : IRandomProvider
    {
        private readonly Random _random;

        public RandomWithHistoryProvider()
        {
            _random = new Random();
        }

        public List<SoundFile> CreateRandomList(List<SoundFile> source, int outputSize)
        {
            List<SoundFile> output = new List<SoundFile>();



            return output;
        }

        public string Name => "Random with History";
        public string Description => "Will utilize user history to not repeat songs in desired time range.  Will repeat if source list is exhausted.";
    }
}
