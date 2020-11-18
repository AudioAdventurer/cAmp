using System;
using System.Collections.Generic;

namespace cAmp.Libraries.Common.Objects
{
    public class Library
    {
        private List<Artist> _artists;
        private Dictionary<string, Artist> _artistsByName;
        private Dictionary<Guid, Artist> _artistsById;

        private List<SoundFile> _soundFiles;
        private Dictionary<Guid, SoundFile> _soundFilesById;
        private Dictionary<string, List<SoundFile>> _soundFilesByName;
        private Dictionary<string, SoundFile> _soundFilesByFileName;

        private List<Album> _albums;
        private Dictionary<Guid, Album> _albumsById;
        private Dictionary<string, List<Album>> _albumsByName;


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
        }

        public IReadOnlyList<Artist> Artists => _artists;
        public IReadOnlyList<Album> Albums => _albums;
        public IReadOnlyList<SoundFile> SoundFiles => _soundFiles;

        public void Add(SoundFile soundFile)
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

        public void Add(Artist artist)
        {
            _artists.Add(artist);
            _artistsById.Add(artist.Id, artist);
            _artistsByName.Add(artist.Name, artist);
        }

        public void Add(Album album)
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

        public bool ContainsArtist(string artistName)
        {
            return _artistsByName.ContainsKey(artistName);
        }

        public Artist GetArtistByName(string artistName)
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

        public Album GetAlbum(Guid albumId)
        {
            return _albumsById[albumId];
        }

    }
}
