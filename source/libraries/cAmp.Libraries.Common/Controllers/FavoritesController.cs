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
        private readonly PlayListService _playListService;
        private readonly IcAmpLogger _logger;

        public FavoritesController(
            PlayListService playListService,
            IcAmpLogger logger)
        {
            _playListService = playListService;
            _logger = logger;
        }

        [HttpPost]
        [Route("api/favorites/soundfile/{soundFileId:Guid}/add")]
        public ActionResult AddToFavoritest(
            [FromRoute] Guid soundFileId)
        {
            var userId = User.GetUserId();
            _playListService.AddSoundFileToFavorites(userId, soundFileId);

            return Ok();
        }

        [HttpPost]
        [Route("api/favorites/soundfile/{soundFileId:Guid}/toggle")]
        public ActionResult ToggleFavorite(
            [FromRoute] Guid soundFileId)
        {
            var userId = User.GetUserId();
            _playListService.ToggleFavorite(userId, soundFileId);

            return Ok();
        }

        [HttpPost]
        [Route("api/favorites/soundfile/{soundFileId:Guid}/remove")]
        public ActionResult RemoveFromFavorites(
            [FromRoute] Guid soundFileId)
        {
            var userId = User.GetUserId();
            _playListService.RemoveSoundFileFromFavorites(userId, soundFileId);

            return Ok();
        }

        [HttpGet]
        [Route("api/favorites/soundfiles")]
        public ActionResult<List<UserInterfaceObjects.SoundFile>> GetSoundFiles(
            [FromRoute] Guid playListId)
        {
            var userId = User.GetUserId();

            var soundFiles = _playListService
                .GetFavoritesSoundFiles(userId)
                .ToUserInterfaceObjects();

            return Ok(soundFiles);
        }

    }
}
