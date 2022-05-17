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
            _logger.Info("GET:api/artists");

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
            _logger.Info($"GET:api/artists/{artistId}/albums");

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
            _logger.Info($"GET:api/artists/{artistId}");

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
            _logger.Info($"GET:api/artists/{artistId}/soundfiles");

            var soundFiles = _libraryService
                .GetSoundFilesByArtist(artistId)
                .ToUserInterfaceObjects();

            _libraryService.ProcessIsFavoriteFlag(User.GetUserId(), soundFiles);

            return Ok(soundFiles);
        }
    }
}
