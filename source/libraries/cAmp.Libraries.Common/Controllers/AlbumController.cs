using System;
using System.Collections.Generic;
using cAmp.Libraries.Common.Helpers;
using cAmp.Libraries.Common.Interfaces;
using cAmp.Libraries.Common.Objects;
using cAmp.Libraries.Common.Services;
using Microsoft.AspNetCore.Mvc;

namespace cAmp.Libraries.Common.Controllers
{
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
            var albums = _libraryService
                .GetAlbums()
                .ToUserInterfaceObjects();

            return Ok(albums);
        }

        [HttpGet]
        [Route("api/albums/{albumId:Guid}")]
        public ActionResult<UserInterfaceObjects.Album> GetAlbum(Guid albumId)
        {
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
            var soundFiles = _libraryService
                .GetSoundFilesByAlbum(albumId)
                .ToUserInterfaceObjects();

            return Ok(soundFiles);
        }
    }
}
