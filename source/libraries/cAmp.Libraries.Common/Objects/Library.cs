﻿using System;
using System.Collections.Generic;
using cAmp.Libraries.Common.Records;

namespace cAmp.Libraries.Common.Objects
{
    public class Library
    {
        private readonly List<Artist> _artists;
        private readonly Dictionary<string, Artist> _artistsByName;
        private readonly Dictionary<Guid, Artist> _artistsById;

        private readonly List<SoundFile> _soundFiles;
        private readonly Dictionary<Guid, SoundFile> _soundFilesById;
        private readonly Dictionary<string, List<SoundFile>> _soundFilesByName;
        private readonly Dictionary<string, SoundFile> _soundFilesByFileName;

        private readonly List<Album> _albums;
        private readonly Dictionary<Guid, Album> _albumsById;
        private readonly Dictionary<string, List<Album>> _albumsByName;

        private readonly Dictionary<Guid, SoundFileQueue> _soundFileQueues;

        public Library()
        {
            _artists = new List<Artist>();
            _artistsByName = new Dictionary<string, Artist>();
            _artistsById = new Dictionary<Guid, Artist>();

            _albums = new List<Album>();
            _albumsByName = new Dictionary<string, List<Album>>();
            _albumsById = new Dictionary<Guid, Album>();

            _soundFiles = new List<SoundFile>();
            _soundFilesByName = new Dictionary<string, List<SoundFile>>();
            _soundFilesById = new Dictionary<Guid, SoundFile>();
            _soundFilesByFileName = new Dictionary<string, SoundFile>();

            _soundFileQueues = new Dictionary<Guid, SoundFileQueue>();
        }

        public IReadOnlyList<Artist> Artists => _artists;
        public IReadOnlyList<Album> Albums => _albums;
        public IReadOnlyList<SoundFile> SoundFiles => _soundFiles;

        public void Add(SoundFile soundFile)
        {
            lock(_soundFiles)
            {
                _soundFiles.Add(soundFile);
                _soundFilesById.Add(soundFile.Id, soundFile);
                _soundFilesByFileName.Add(soundFile.Filename, soundFile);

                //Find the list of files of this name if it exists
                List<SoundFile> files;
                if (_soundFilesByName.ContainsKey(soundFile.Title))
                {
                    files = _soundFilesByName[soundFile.Title];
                }
                else
                {
                    //Create one if it doesn't
                    files = new List<SoundFile>();
                    _soundFilesByName.Add(soundFile.Title, files);
                }

                //Add the file to the list
                files.Add(soundFile);
            }            
        }

        public void Add(Artist artist)
        {
            lock(_artists)
            {
                _artists.Add(artist);
                _artistsById.Add(artist.Id, artist);
                _artistsByName.Add(artist.Name.ToLower(), artist);
            }
        }

        public void Add(Album album)
        {
            lock(_albums)
            {
                _albums.Add(album);
                _albumsById.Add(album.Id, album);

                //Find the list of files of this name if it exists
                List<Album> albums;
                if (_albumsByName.ContainsKey(album.Name))
                {
                    albums = _albumsByName[album.Name];
                }
                else
                {
                    //Create one if it doesn't
                    albums = new List<Album>();
                    _albumsByName.Add(album.Name, albums);
                }

                //Add the file to the list
                albums.Add(album);
            }
        }

        public bool ContainsArtist(string artistName)
        {
            return _artistsByName.ContainsKey(artistName.ToLower());
        }

        public Artist GetArtistByName(string artistName)
        {
            var lowerArtist = artistName.ToLower();

            if (_artistsByName.ContainsKey(lowerArtist))
            {
                return _artistsByName[lowerArtist];
            }

            return null;
        }

        public Artist GetArtist(Guid artistId)
        {
            if (_artistsById.ContainsKey(artistId))
            {
                return _artistsById[artistId];
            }

            return null;
        }

        public bool ContainsSoundFile(Guid soundFileId)
        {
            return _soundFilesById.ContainsKey(soundFileId);
        }

        public SoundFile GetSoundFile(Guid soundFileId)
        {
            if (_soundFilesById.ContainsKey(soundFileId))
            {
                return _soundFilesById[soundFileId];
            }

            return null;
        }

        public SoundFile GetSoundFile(string filename)
        {
            if (_soundFilesByFileName.ContainsKey(filename))
            {
                return _soundFilesByFileName[filename];
            }

            return null;
        }

        public Album GetAlbum(Guid albumId)
        {
            if (_albumsById.ContainsKey(albumId))
            {
                return _albumsById[albumId];
            }

            return null;
        }

        public SoundFileQueue GetQueueByUser(Guid userId)
        {
            lock (_soundFileQueues)
            {
                if (_soundFileQueues.ContainsKey(userId))
                {
                    return _soundFileQueues[userId];
                }

                SoundFileQueue queue = new SoundFileQueue();
                _soundFileQueues.Add(userId, queue);
                return queue;
            }
        }

        public void ClearQueue(Guid userId)
        {
            var queue = GetQueueByUser(userId);
            queue.Clear();
        }

        public void ShuffleQueue(Guid userId)
        {
            var queue = GetQueueByUser(userId);
            queue.Shuffle();
        }

        public List<SoundFile> GetQueueSoundFiles(Guid userId)
        {
            var queue = GetQueueByUser(userId);

            return queue.ToList();
        }

        public SoundFile GetQueueNextSoundFile(Guid userId)
        {
            var queue = GetQueueByUser(userId);

            return queue.NextSoundFile();
        }

        public SoundFile GetQueueCurrentSoundFile(Guid userId)
        {
            var queue = GetQueueByUser(userId);

            return queue.CurrentSoundFile;
        }

        public SoundFile AdvanceQueue(Guid userId)
        {
            var queue = GetQueueByUser(userId);

            return queue.AdvanceToNextSoundFile();
        }

        public void AddSongToQueue(Guid userId, Guid soundFileId)
        {
            var queue = GetQueueByUser(userId);

            if (_soundFilesById.ContainsKey(soundFileId))
            {
                var soundFile = _soundFilesById[soundFileId];

                queue.Enqueue(soundFile);
            }
        }

        public void AddAlbumToQueue(Guid userId, Guid albumId)
        {
            var queue = GetQueueByUser(userId);

            if (_albumsById.ContainsKey(albumId))
            {
                var album = _albumsById[albumId];

                queue.Enqueue(album.Songs);
            }
        }

        public void AddArtistToQueue(Guid userId, Guid artistId)
        {
            var queue = GetQueueByUser(userId);

            if (_artistsById.ContainsKey(artistId))
            {
                var artist = _artistsById[artistId];

                foreach (var album in artist.Albums)
                {
                    queue.Enqueue(album.Songs);
                }
            }
        }

        public int GetQueueSize(Guid userId)
        {
            var queue = GetQueueByUser(userId);

            return queue.QueueSize;
        }
    }
}
