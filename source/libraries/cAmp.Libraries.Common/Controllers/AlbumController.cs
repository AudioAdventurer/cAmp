using System;
using System.Collections.Generic;
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
        public ActionResult<Album> GetAlbums()
        {
            var albums = _libraryService.GetAlbums();
            return Ok(albums);
        }

        [HttpGet]
        [Route("api/albums/{albumId:Guid}")]
        public ActionResult<Album> GetAlbum(Guid albumId)
        {
            var album = _libraryService.GetAlbum(albumId);
            return Ok(album);
        }

        [HttpGet]
        [Route("api/albums/{albumId:Guid}/soundfiles")]
        public ActionResult<List<SoundFile>> GetSoundFiles(
            [FromRoute] Guid albumId)
        {
            var soundFiles = _libraryService.GetSoundFilesByAlbum(albumId);
            return Ok(soundFiles);
        }
    }
}
