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
        private readonly PlayListService _playListService;
        private readonly IcAmpLogger _logger;

        public PlayListController(
            PlayListService playListService,
            IcAmpLogger logger)
        {
            _playListService = playListService;
            _logger = logger;
        }


        [HttpGet]
        [Route("api/playlists")]
        public ActionResult<List<UserInterfaceObjects.PlayList>> GetPlayLists()
        {
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
            var userId = User.GetUserId();

            _playListService
                .SavePlayList(userId, playList);

            return Ok();
        }

        [HttpDelete]
        [Route("api/playlists/{playListId:Guid}")]
        public ActionResult DeletePlayList([FromRoute] Guid playListId)
        {
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
            var userId = User.GetUserId();
            _playListService.RemoveSoundFileFromPlayList(userId, playListId, soundFileId);

            return Ok();
        }

        [HttpGet]
        [Route("api/playlists/{playListId:Guid}/soundfiles")]
        public ActionResult<List<UserInterfaceObjects.SoundFile>> GetSoundFiles(
            [FromRoute] Guid playListId)
        {
            var userId = User.GetUserId();

            var soundFiles = _playListService
                .GetSoundFiles(userId, playListId)
                .ToUserInterfaceObjects();

            return Ok(soundFiles);
        }
    }
}
