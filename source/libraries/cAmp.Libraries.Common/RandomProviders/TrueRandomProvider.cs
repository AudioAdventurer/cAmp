using System;
using System.Collections.Generic;
using cAmp.Libraries.Common.Interfaces;
using cAmp.Libraries.Common.Objects;

namespace cAmp.Libraries.Common.RandomProviders
{
    public class TrueRandomProvider : IRandomProvider
    {
        private readonly Random _random;

        public TrueRandomProvider()
        {
            _random = new Random();
        }

        public List<SoundFile> CreateRandomList(List<SoundFile> source, int outputSize)
        {
            var output = new List<SoundFile>();

            int min = 0;
            int max = source.Count;

            for (int i = 0; i < outputSize; i++)
            {
                int choicePosition = _random.Next(min, max);
                var choice = source[choicePosition];
                output.Add(choice);
            }
            
            return output;
        }

        public string Name => "True Random Provider";
        public string Description => "Randomly selects songs from the source.  Can end up with repeated songs.";
    }
}
