﻿using System.Collections.Generic;
using System.Linq;
using cAmp.Libraries.Common.Records;

namespace cAmp.Libraries.Common.Objects
{
    public class SoundFileQueue
    {
        private readonly Queue<SoundFile> _queue;
        private SoundFile _currentSoundFile;
        private object _lock = new object();

        public SoundFileQueue()
        {
            _queue = new Queue<SoundFile>();
        }

        public int QueueSize
        {
            get
            {
                if (_currentSoundFile != null)
                {
                    return _queue.Count + 1;
                }
                else
                {
                    return _queue.Count;
                }
            }
        }

        public List<SoundFile> ToList()
        {
            lock (_lock)
            {
                var output = _queue.ToList();

                if (_currentSoundFile != null)
                {
                    output.Insert(0, _currentSoundFile);
                }

                return output;
            }
        }

        public void Clear()
        {
            lock (_lock)
            {
                _queue.Clear();
                _currentSoundFile = null;
            }
        }

        public SoundFile NextSoundFile()
        {
            lock (_lock)
            {
                if (_queue.Count > 0)
                {
                    return _queue.Peek();
                }

                return null;
            }
        }

        public SoundFile CurrentSoundFile => _currentSoundFile;

        public SoundFile AdvanceToNextSoundFile()
        {
            lock (_lock)
            {
                if (_queue.Count > 0)
                {
                    var temp = _queue.Dequeue();

                    if (temp == _currentSoundFile)
                    {
                        _queue.Dequeue();
                    }

                    _currentSoundFile = temp;
                }
                else
                {
                    _currentSoundFile = null;
                }

                return _currentSoundFile;
            }

        }

        public void Enqueue(SoundFile soundFile)
        {
            lock (_lock)
            {
                _queue.Enqueue(soundFile);

                EnsureCurrentSoundFileSet();
            }
        }

        public void Enqueue(List<SoundFile> soundFiles)
        {
            lock (_lock)
            {
                foreach (var soundFile in soundFiles)
                {
                    _queue.Enqueue(soundFile);
                }

                EnsureCurrentSoundFileSet();
            }
        }

        public void EnsureCurrentSoundFileSet()
        {
            lock (_lock)
            {
                if (_currentSoundFile == null
                    && _queue.Count > 0)
                {
                    _currentSoundFile = _queue.Dequeue();
                }
            }
        }
    }
}
