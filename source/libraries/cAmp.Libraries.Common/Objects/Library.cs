using System;
using System.Collections.Generic;

namespace cAmp.Libraries.Common.Objects
{
    public class Library
    {
        private List<Artist> _artists;
        private SortedList<string, Artist> _artistsByName;
        private Dictionary<Guid, Artist> _artistsById;

        private List<SoundFile> _soundFiles;
        private Dictionary<Guid, SoundFile> _soundFilesById;
        private SortedList<string, SoundFile> _soundFilesByName;

        private List<Album> _albums;
        private Dictionary<Guid, Album> _albumsById;
        private SortedList<string, Album> _albumsByName;


        public Library()
        {
            _artists = new List<Artist>();
            _artistsByName = new SortedList<string, Artist>();
            _artistsById = new Dictionary<Guid, Artist>();

            _albums = new List<Album>();
            _albumsByName = new SortedList<string, Album>();
            _albumsById = new Dictionary<Guid, Album>();

            _soundFiles = new List<SoundFile>();
            _soundFilesByName = new SortedList<string, SoundFile>();
            _soundFilesById = new Dictionary<Guid, SoundFile>();
        }

        public IReadOnlyList<Artist> Artists => _artists;
        public IReadOnlyList<Album> Albums => _albums;
        public IReadOnlyList<SoundFile> SoundFiles => _soundFiles;

        public void Add(SoundFile soundFile)
        {
            _soundFiles.Add(soundFile);
            _soundFilesById.Add(soundFile.Id, soundFile);
            _soundFilesByName.Add(soundFile.Filename, soundFile);
        }

        public void Add(Artist artist)
        {
            _artists.Add(artist);
            _artistsByName.Add(artist.Name, artist);
            _artistsById.Add(artist.Id, artist);
        }

        public void Add(Album album)
        {
            _albums.Add(album);
            _albumsByName.Add(album.Name, album);
            _albumsById.Add(album.Id, album);
        }

        public bool ContainsArtist(string artistName)
        {
            return _artistsByName.ContainsKey(artistName);
        }

        public Artist GetArtist(string artistName)
        {
            return _artistsByName[artistName];
        }

        public Artist GetArtist(Guid artistId)
        {
            return _artistsById[artistId];
        }

        public bool ContainsSoundFile(Guid soundFileId)
        {
            return _soundFilesById.ContainsKey(soundFileId);
        }

        public SoundFile GetSoundFile(Guid soundFileId)
        {
            return _soundFilesById[soundFileId];
        }

    }
}
