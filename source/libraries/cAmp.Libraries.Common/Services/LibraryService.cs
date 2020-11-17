﻿using System;
using System.Collections.Generic;
using System.Linq;
using cAmp.Libraries.Common.Interfaces;
using cAmp.Libraries.Common.Objects;

namespace cAmp.Libraries.Common.Services
{
    public class LibraryService
    {
        private readonly Library _library;
        private readonly IcAmpLogger _logger;

        public LibraryService(
            Library library,
            IcAmpLogger logger)
        {
            _library = library;
            _logger = logger;
        }


        public List<SoundFile> GetSoundFiles()
        {
            return _library.SoundFiles.OrderBy(s => s.Title).ToList();
        }

        public List<SoundFile> GetSoundFilesByArtist(Guid artistId)
        {
            var artist = _library.GetArtist(artistId);

            List<SoundFile> soundFiles = new List<SoundFile>();

            foreach (Album album in  artist.Albums.OrderBy(a=>a.Name))
            {
                foreach (var soundFile in album.Songs.OrderBy(s => s.TrackNumber))
                {
                    soundFiles.Add(soundFile);
                }
            }

            return soundFiles;
        }

        public List<SoundFile> GetSoundFilesByAlbum(Guid albumId)
        {
            var album = _library.GetAlbum(albumId);

            List<SoundFile> soundFiles = new List<SoundFile>();

            foreach (var soundFile in album.Songs.OrderBy(s => s.TrackNumber))
            {
                soundFiles.Add(soundFile);
            }

            return soundFiles;
        }

        public Album GetAlbum(Guid albumId)
        {
            return _library.GetAlbum(albumId);
        }

        public List<Album> GetAlbums()
        {
            return _library.Albums
                .OrderBy(a => a.Name)
                .ToList();
        }

        public List<Album> GetAlbumsByArtist(Guid artistId)
        {
            var artist = _library.GetArtist(artistId);

            return artist.Albums
                .OrderBy(a => a.Name)
                .ToList();
        }

        public List<Artist> GetArtists()
        {
            return _library.Artists
                .OrderBy(a => a.Name)
                .ToList();
        }

        public Artist GetArtist(Guid artistId)
        {
            return _library.GetArtist(artistId);
        }
    }
}