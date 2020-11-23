using System;
using System.Collections.Generic;
using cAmp.Libraries.Common.Helpers;
using cAmp.Libraries.Common.Interfaces;
using cAmp.Libraries.Common.Objects;

namespace cAmp.Libraries.Common.RandomProviders
{
    public class RandomWithSourceDrainProvider : IRandomProvider
    {
        private readonly Random _random;

        public RandomWithSourceDrainProvider()
        {
            _random = new Random();
        }

        public List<SoundFile> CreateRandomList(List<SoundFile> source, int outputSize)
        {
            var output = new List<SoundFile>();

            var sourceCopy = source.Copy();
            
            for (int i = 0; i < outputSize; i++)
            {
                int min = 0;
                int max = sourceCopy.Count;

                int choicePosition = _random.Next(min, max);
                var choice = sourceCopy[choicePosition];
                output.Add(choice);

                sourceCopy.RemoveAt(choicePosition);

                //If desired output is greater than source then reuse source
                if (sourceCopy.Count == 0)
                {
                    sourceCopy = source.Copy();
                }
            }

            return output;
        }

        public string Name => "Random with Source Draining";
        public string Description => "Will only repeat songs if the output size is greater than the source size.";
    }
}
