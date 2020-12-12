using System;
using System.Collections.Generic;
using System.Linq;
using cAmp.Libraries.Common.Interfaces;
using cAmp.Libraries.Common.Objects;
using cAmp.Libraries.Common.Repos;

namespace cAmp.Libraries.Common.Services
{
    public class PlayListService
    {
        private readonly PlayListRepo _playListRepo;
        private readonly PlayListSoundFileRepo _playListSoundFileRepo;
        private readonly Library _library;
        private readonly IcAmpLogger _logger;

        public PlayListService(
            PlayListRepo playListRepo,
            PlayListSoundFileRepo playListSoundFileRepo,
            Library library,
            IcAmpLogger logger)
        {
            _playListRepo = playListRepo;
            _playListSoundFileRepo = playListSoundFileRepo;
            _library = library;
            _logger = logger;
        }

        public List<PlayList> GetPlayLists(Guid userId)
        {
            _playListRepo.EnsureFavoritesPlayList(userId);

            var playLists = _playListRepo.GetByUser(userId);
            return playLists.ToList();
        }

        public void SavePlayList(Guid userId, UserInterfaceObjects.PlayList uiPlayList)
        {
            if (uiPlayList.Name.Equals("Favorites", StringComparison.InvariantCultureIgnoreCase))
            {
                throw new Exception("Unable to save built in playlist");
            }

            var playList = new PlayList
            {
                Id = uiPlayList.Id,
                Name = uiPlayList.Name,
                Description = uiPlayList.Description,
                OwnerUserId = userId
            };

            _playListRepo.Save(playList);
        }

        public void DeletePlayList(Guid userId, Guid playListId)
        {
            var playList = _playListRepo.GetById(playListId);

            if (playList != null)
            {
                if (playList.OwnerUserId != userId)
                {
                    throw new Exception("Not your playlist.");
                }

                if (playList.Name == "Favorites")
                {
                    throw new Exception("Unable to delete built in playlist");
                }

                _playListSoundFileRepo.DeletePlayList(playList.Id);
                _playListRepo.Delete(playList.Id);
            }
        }

        public void AddSoundFileToPlayList(Guid userId, Guid playListId, Guid soundFileId)
        {
            var soundFile = _library.GetSoundFile(soundFileId);

            if (soundFile != null)
            {
                var plsf = _playListSoundFileRepo.GetByPlayListFileName(playListId, soundFile.Filename);

                if (plsf == null)
                {
                    plsf = new PlayListSoundFile
                    {
                        Filename = soundFile.Filename,
                        PlayListId = playListId
                    };

                    _playListSoundFileRepo.Save(plsf);
                }
            }
        }

        public void RemoveSoundFileFromPlayList(Guid userId, Guid playListId, Guid soundFileId)
        {
            var soundFile = _library.GetSoundFile(soundFileId);

            if (soundFile != null)
            {
                var plsf = _playListSoundFileRepo.GetByPlayListFileName(playListId, soundFile.Filename);

                if (plsf != null)
                {
                    _playListSoundFileRepo.Delete(plsf.Id);
                }
            }
        }

        public List<SoundFile> GetSoundFiles(
            Guid userId,
            Guid playListId)
        {
            var output = new List<SoundFile>();

            var playList = _playListRepo.GetById(playListId);

            if (playList != null)
            {
                if (playList.OwnerUserId == userId)
                {
                    var plsfs = _playListSoundFileRepo.GetByPlayList(playListId);

                    foreach (var plsf in plsfs)
                    {
                        var soundFile = _library.GetSoundFile(plsf.Filename);

                        if (soundFile != null)
                        {
                            output.Add(soundFile);
                        }
                    }
                }
            }

            return output;
        }
    }
}
