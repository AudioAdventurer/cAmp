using System;
using System.Collections.Generic;
using cAmp.Libraries.Common.Interfaces;
using cAmp.Libraries.Common.Objects;
using cAmp.Libraries.Common.Services;
using Microsoft.AspNetCore.Mvc;

namespace cAmp.Libraries.Common.Controllers
{
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
        public ActionResult<Artist> GetArtists()
        {
            var artist = _libraryService.GetArtists();
            return Ok(artist);
        }

        [HttpGet]
        [Route("api/artists/{artistId:Guid}")]
        public ActionResult<Artist> GetArtist(
            [FromRoute] Guid artistId)
        {
            var artist = _libraryService.GetArtist(artistId);
            return Ok(artist);
        }

        [HttpGet]
        [Route("api/artists/{artistId:Guid}/soundfiles")]
        public ActionResult<List<SoundFile>> GetSoundFiles(
            [FromRoute] Guid artistId)
        {
            var soundFiles = _libraryService.GetSoundFilesByArtist(artistId);
            return Ok(soundFiles);
        }
    }
}
