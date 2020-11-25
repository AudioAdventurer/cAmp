using System;
using System.Collections.Generic;
using cAmp.Libraries.Common.Helpers;
using cAmp.Libraries.Common.Interfaces;
using cAmp.Libraries.Common.Objects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cAmp.Libraries.Common.Controllers
{
    [Authorize]
    [Produces("application/json")]
    public class QueueController : ControllerBase
    {
        private readonly Library _library;
        private readonly IcAmpLogger _logger;

        public QueueController(
            Library library,
            IcAmpLogger logger)
        {
            _library = library;
            _logger = logger;
        }

        [HttpPost]
        [Route("api/queue/song/{soundFileId:Guid}")]
        public ActionResult AddSongToQueue([FromRoute] Guid soundFileId)
        {
            Guid userId = User.GetUserId();

            _library.AddSongToQueue(userId, soundFileId);

            return Ok();
        }

        [HttpPost]
        [Route("api/queue/album/{albumId:Guid}")]
        public ActionResult AddAlbumToQueue([FromRoute] Guid albumId)
        {
            Guid userId = User.GetUserId();

            _library.AddAlbumToQueue(userId, albumId);

            return Ok();
        }

        [HttpPost]
        [Route("api/queue/artist/{artistId:Guid}")]
        public ActionResult AddArtistToQueue([FromRoute] Guid artistId)
        {
            Guid userId = User.GetUserId();

            _library.AddArtistToQueue(userId, artistId);

            return Ok();
        }

        [HttpGet]
        [Route("api/queue")]
        public ActionResult<List<UserInterfaceObjects.SoundFile>> GetQueue()
        {
            Guid userId = User.GetUserId();

            var soundFiles = _library.GetQueueSoundFiles(userId)
                .ToUserInterfaceObjects();

            return Ok(soundFiles);
        }

        [HttpGet]
        [Route("api/queue/next")]
        public ActionResult<List<UserInterfaceObjects.SoundFile>> GetNextSoundFile()
        {
            Guid userId = User.GetUserId();

            var soundFile = _library.GetQueueNextSoundFile(userId)?
                .ToUserInterfaceObject();

            return Ok(soundFile);
        }

        [HttpGet]
        [Route("api/queue/current")]
        public ActionResult<UserInterfaceObjects.SoundFile> GetCurrentSoundFile()
        {
            Guid userId = User.GetUserId();

            var soundFile = _library.GetQueueCurrentSoundFile(userId)?
                .ToUserInterfaceObject();

            return Ok(soundFile);
        }

        [HttpGet]
        [Route("api/queue/advance")]
        public ActionResult<UserInterfaceObjects.SoundFile> AdvanceToNextSoundFile()
        {
            Guid userId = User.GetUserId();

            var soundFile = _library.AdvanceQueue(userId)?
                .ToUserInterfaceObject();

            return Ok(soundFile);
        }

        [HttpGet]
        [Route("api/queue/size")]
        public ActionResult<int> GetQueueSize()
        {
            Guid userId = User.GetUserId();

            var size = _library.GetQueueSize(userId);

            return Ok(size);
        }

        [HttpGet]
        [Route("api/queue/clear")]
        public ActionResult ClearQueue()
        {
            Guid userId = User.GetUserId();

            _library.ClearQueue(userId);

            return Ok();
        }
    }
}
