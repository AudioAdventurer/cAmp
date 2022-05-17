using System;
using System.Collections.Generic;
using System.Text;
using cAmp.Libraries.Common.Helpers;
using cAmp.Libraries.Common.Interfaces;
using cAmp.Libraries.Common.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cAmp.Libraries.Common.Controllers
{
    [Authorize]
    [Produces("application/json")]
    public class FavoritesController : ControllerBase
    {
        private readonly LibraryService _libraryService;
        private readonly PlayListService _playListService;
        private readonly IcAmpLogger _logger;

        public FavoritesController(
            LibraryService libraryService,
            PlayListService playListService,
            IcAmpLogger logger)
        {
            _libraryService = libraryService;
            _playListService = playListService;
            _logger = logger;
        }

        [HttpPost]
        [Route("api/favorites/soundfile/{soundFileId:Guid}/add")]
        public ActionResult AddToFavoritest(
            [FromRoute] Guid soundFileId)
        {
            _logger.Info($"POST:api/favorites/soundfile/{soundFileId}/add");

            var userId = User.GetUserId();
            _playListService.AddSoundFileToFavorites(userId, soundFileId);

            return Ok();
        }

        [HttpPost]
        [Route("api/favorites/soundfile/{soundFileId:Guid}/toggle")]
        public ActionResult ToggleFavorite(
            [FromRoute] Guid soundFileId)
        {
            _logger.Info($"POST:api/favorites/soundfile/{soundFileId}/toggle");

            var userId = User.GetUserId();
            _playListService.ToggleFavorite(userId, soundFileId);

            return Ok();
        }

        [HttpPost]
        [Route("api/favorites/soundfile/{soundFileId:Guid}/remove")]
        public ActionResult RemoveFromFavorites(
            [FromRoute] Guid soundFileId)
        {
            _logger.Info($"POST:api/favorites/soundfile/{soundFileId}/remove");

            var userId = User.GetUserId();
            _playListService.RemoveSoundFileFromFavorites(userId, soundFileId);

            return Ok();
        }

        [HttpGet]
        [Route("api/favorites/soundfiles")]
        public ActionResult<List<UserInterfaceObjects.SoundFile>> GetSoundFiles()
        {
            _logger.Info("GET:api/favorites/soundfiles");

            var userId = User.GetUserId();

            var soundFiles = _playListService
                .GetFavoritesSoundFiles(userId)
                .ToUserInterfaceObjects();

            _libraryService.ProcessIsFavoriteFlag(User.GetUserId(), soundFiles);

            return Ok(soundFiles);
        }

    }
}
