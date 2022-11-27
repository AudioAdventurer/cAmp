using System;
using System.Collections.Generic;
using cAmp.Libraries.Common.Helpers;
using cAmp.Libraries.Common.Interfaces;
using cAmp.Libraries.Common.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cAmp.Libraries.Common.Controllers
{
    [Authorize]
    [Produces("application/json")]
    public class PlayListController : ControllerBase
    {
        private readonly LibraryService _libraryService;
        private readonly PlayListService _playListService;
        private readonly IcAmpLogger _logger;

        public PlayListController(
            LibraryService libraryService,
            PlayListService playListService,
            IcAmpLogger logger)
        {
            _libraryService = libraryService;
            _playListService = playListService;
            _logger = logger;
        }


        [HttpGet]
        [Route("api/playlists")]
        public ActionResult<List<UserInterfaceObjects.PlayList>> GetPlayLists()
        {
            _logger.Info($"GET:api/playlists");

            var userId = User.GetUserId();

            var playLists = _playListService
                .GetPlayLists(userId)
                .ToUserInterfaceObjects();

            return Ok(playLists);
        }

        [HttpPost]
        [Route("api/playlists")]
        public ActionResult SavePlayList([FromBody] UserInterfaceObjects.PlayList playList)
        {
            _logger.Info($"POST:api/playlists");
            var userId = User.GetUserId();

            _playListService
                .SavePlayList(userId, playList);

            return Ok();
        }

        [HttpGet]
        [Route("api/playlists/{playListId:Guid}")]
        public ActionResult<UserInterfaceObjects.PlayList> GetPlayList([FromRoute] Guid playListId)
        {
            _logger.Info($"GET:api/playlists/{playListId}");

            var userId = User.GetUserId();

            var playList = _playListService
                .GetPlayList(userId, playListId)
                .ToUserInterfaceObject();

            return Ok(playList);
        }

        [HttpDelete]
        [Route("api/playlists/{playListId:Guid}")]
        public ActionResult DeletePlayList([FromRoute] Guid playListId)
        {
            _logger.Info($"DELETE:api/playlists/{playListId}");

            var userId = User.GetUserId();

            _playListService
                .DeletePlayList(userId, playListId);

            return Ok();
        }
        

        [HttpPost]
        [Route("api/playlists/{playListId:Guid}/soundfile/{soundFileId:Guid}/add")]
        public ActionResult AddToPlayList(
            [FromRoute] Guid playListId, 
            [FromRoute] Guid soundFileId)
        {
            _logger.Info($"POST:api/playlists/{playListId}/soundfile/{soundFileId}/add");

            var userId = User.GetUserId();
            _playListService.AddSoundFileToPlayList(userId, playListId, soundFileId);

            return Ok();
        }

        [HttpPost]
        [Route("api/playlists/{playListId:Guid}/soundfile/{soundFileId:Guid}/remove")]
        public ActionResult RemoveFromPlayList(
            [FromRoute] Guid playListId,
            [FromRoute] Guid soundFileId)
        {
            _logger.Info($"POST:api/playlists/{playListId}/soundfile/{soundFileId}/remove");

            var userId = User.GetUserId();
            _playListService.RemoveSoundFileFromPlayList(userId, playListId, soundFileId);

            return Ok();
        }

        [HttpGet]
        [Route("api/playlists/{playListId:Guid}/soundfiles")]
        public ActionResult<List<UserInterfaceObjects.SoundFile>> GetSoundFiles(
            [FromRoute] Guid playListId)
        {
            _logger.Info($"GET:api/playlists/{playListId}/soundfiles");

            var userId = User.GetUserId();

            var soundFiles = _playListService
                .GetSoundFiles(userId, playListId)
                .ToUserInterfaceObjects();

            _libraryService.ProcessIsFavoriteFlag(User.GetUserId(), soundFiles);

            return Ok(soundFiles);
        }
    }
}
