using System;
using System.Collections.Generic;
using cAmp.Libraries.Common.Helpers;
using cAmp.Libraries.Common.Interfaces;
using cAmp.Libraries.Common.Objects;
using cAmp.Libraries.Common.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cAmp.Libraries.Common.Controllers
{
    [Authorize]
    [Produces("application/json")]
    public class ArtistController : ControllerBase
    {
        private readonly LibraryService _libraryService;
        private readonly IcAmpLogger _logger;

        public ArtistController(
            LibraryService libraryService,
            IcAmpLogger logger)
        {
            _libraryService = libraryService;
            _logger = logger;
        }

        [HttpGet]
        [Route("api/artists")]
        public ActionResult<UserInterfaceObjects.Artist> GetArtists()
        {
            var artist = _libraryService
                .GetArtists()
                .ToUserInterfaceObjects();

            return Ok(artist);
        }

        [HttpGet]
        [Route("api/artists/{artistId:Guid}/albums")]
        public ActionResult<UserInterfaceObjects.Artist> GetAlbums(
            [FromRoute] Guid artistId)
        {
            var albums = _libraryService
                .GetAlbumsByArtist(artistId)
                .ToUserInterfaceObjects();

            return Ok(albums);
        }

        [HttpGet]
        [Route("api/artists/{artistId:Guid}")]
        public ActionResult<UserInterfaceObjects.Artist> GetArtist(
            [FromRoute] Guid artistId)
        {
            var artist = _libraryService
                .GetArtist(artistId)
                .ToUserInterfaceObject();

            return Ok(artist);
        }

        [HttpGet]
        [Route("api/artists/{artistId:Guid}/soundfiles")]
        public ActionResult<List<UserInterfaceObjects.SoundFile>> GetSoundFiles(
            [FromRoute] Guid artistId)
        {
            var soundFiles = _libraryService
                .GetSoundFilesByArtist(artistId)
                .ToUserInterfaceObjects();

            return Ok(soundFiles);
        }
    }
}
