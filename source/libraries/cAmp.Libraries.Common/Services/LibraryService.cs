using System;
using System.Collections.Generic;
using System.Linq;
using cAmp.Libraries.Common.Interfaces;
using cAmp.Libraries.Common.Objects;
using cAmp.Libraries.Common.Repos;

namespace cAmp.Libraries.Common.Services
{
    public class LibraryService
    {
        private readonly Library _library;
        private readonly PlayHistoryRepo _playHistoryRepo;
        private readonly PlayListService _playListService;
        private readonly IcAmpLogger _logger;

        public LibraryService(
            Library library,
            PlayHistoryRepo playHistoryRepo,
            PlayListService playListService,
            IcAmpLogger logger)
        {
            _library = library;
            _playHistoryRepo = playHistoryRepo;
            _playListService = playListService;
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

        public void FinishedSoundFile(Guid userId, Guid soundFileId, bool playedToEnd)
        {
            var soundFile = _library.GetSoundFile(soundFileId);

            var history = new PlayHistory
            {
                Filename = soundFile.Filename,
                Ended = DateTime.UtcNow,
                PlayedToEnd = playedToEnd,
                UserId = userId
            };

            _playHistoryRepo.Save(history);
        }

        public void ProcessIsFavoriteFlag(Guid userId, List<IcAmpObject> cAmpObjects)
        {
            List<UserInterfaceObjects.SoundFile> soundFiles = cAmpObjects.Cast<UserInterfaceObjects.SoundFile>().ToList();

            _playListService.ProcessIsFavoriteFlag(userId, soundFiles);
        }

        public void ProcessIsFavoriteFlag(Guid userId, IcAmpObject cAmpObject)
        {
            List<UserInterfaceObjects.SoundFile> soundFiles = new List<UserInterfaceObjects.SoundFile>();
            soundFiles.Add((UserInterfaceObjects.SoundFile) cAmpObject);

            _playListService.ProcessIsFavoriteFlag(userId, soundFiles);
        }
    }
}
