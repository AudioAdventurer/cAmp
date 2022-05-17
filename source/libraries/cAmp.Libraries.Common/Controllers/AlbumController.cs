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
    public class AlbumController : ControllerBase
    {
        private readonly LibraryService _libraryService;
        private readonly IcAmpLogger _logger;

        public AlbumController(
            LibraryService libraryService,
            IcAmpLogger logger)
        {
            _libraryService = libraryService;
            _logger = logger;
        }

        [HttpGet]
        [Route("api/albums")]
        public ActionResult<UserInterfaceObjects.Album> GetAlbums()
        {
            _logger.Info("GET:api/albums");

            var albums = _libraryService
                .GetAlbums()
                .ToUserInterfaceObjects();

            return Ok(albums);
        }

        [HttpGet]
        [Route("api/albums/{albumId:Guid}")]
        public ActionResult<UserInterfaceObjects.Album> GetAlbum(Guid albumId)
        {
            _logger.Info($"GET:api/albums/{albumId}");

            var album = _libraryService
                .GetAlbum(albumId)
                .ToUserInterfaceObject();

            return Ok(album);
        }

        [HttpGet]
        [Route("api/albums/{albumId:Guid}/soundfiles")]
        public ActionResult<List<UserInterfaceObjects.SoundFile>> GetSoundFiles(
            [FromRoute] Guid albumId)
        {
            _logger.Info($"GET:api/albums/{albumId}/soundfiles");

            var soundFiles = _libraryService
                .GetSoundFilesByAlbum(albumId)
                .ToUserInterfaceObjects();

            _libraryService.ProcessIsFavoriteFlag(User.GetUserId(), soundFiles);

            return Ok(soundFiles);
        }
    }
}
