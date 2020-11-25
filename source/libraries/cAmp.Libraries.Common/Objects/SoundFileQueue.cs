using System.Collections.Generic;
using System.Linq;

namespace cAmp.Libraries.Common.Objects
{
    public class SoundFileQueue
    {
        private readonly Queue<SoundFile> _queue;
        private SoundFile _currentSoundFile;

        public SoundFileQueue()
        {
            _queue = new Queue<SoundFile>();
        }

        public int QueueSize => _queue.Count;

        public List<SoundFile> ToList()
        {
            return _queue.ToList();
        }

        public void Clear()
        {
            _queue.Clear();
        }

        public SoundFile NextSoundFile()
        {
            if (_queue.Count > 0)
            {
                return _queue.Peek();
            }

            return null;
        }

        public SoundFile CurrentSoundFile => _currentSoundFile;

        public SoundFile AdvanceToNextSoundFile()
        {
            if (_queue.Count > 0)
            {
                _currentSoundFile = _queue.Dequeue();
            }
            else
            {
                _currentSoundFile = null;
            }
        
            return _currentSoundFile;
        }

        public void Enqueue(SoundFile soundFile)
        {
            _queue.Enqueue(soundFile);
        }

        public void Enqueue(List<SoundFile> soundFiles)
        {
            foreach (var soundFile in soundFiles)
            {
                _queue.Enqueue(soundFile);
            }
        }
    }
}
