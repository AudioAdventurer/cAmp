using System;
using System.Collections.Generic;
using System.IO;
using cAmp.Libraries.Common.Helpers;
using cAmp.Libraries.Common.Interfaces;
using cAmp.Libraries.Common.Objects;
using cAmp.Libraries.Common.Services;
using Microsoft.AspNetCore.Mvc;

namespace cAmp.Libraries.Common.Controllers
{
    public class SoundFileController : ControllerBase
    {
        private readonly FileService _fileService;
        private readonly LibraryService _libraryService;
        private readonly IcAmpLogger _logger;
        
        public SoundFileController(
            FileService fileService,
            LibraryService libraryService,
            IcAmpLogger logger)
        {
            _fileService = fileService;
            _libraryService = libraryService;
            _logger = logger;
        }

        [HttpGet]
        [Route("api/soundfiles/{soundFileId:Guid}")]
        public ActionResult GetSoundFile([FromRoute] Guid soundFileId)
        {
            using (var stream = _fileService.GetFile(soundFileId))
            {
                byte[] data = null;

                if (stream != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        stream.CopyTo(memoryStream);
                        data = memoryStream.ToArray();
                    }

                    return File(data, "audio/mp3");
                }
            }

            return NotFound();
        }

        [HttpGet]
        [Route("api/soundfiles")]
        public ActionResult<List<UserInterfaceObjects.SoundFile>> GetSoundFiles()
        {
            var soundFiles = _libraryService
                .GetSoundFiles()
                .ToUserInterfaceObjects();

            return Ok(soundFiles);
        }

        [HttpPost]
        [Route("api/soundfiles/{soundFileId:Guid}/completed")]
        public ActionResult FinishedSoundFile([FromRoute] Guid soundFileId)
        {
            _libraryService.FinishedSoundFile(soundFileId, true);
            return Ok();
        }

        [HttpPost]
        [Route("api/soundfiles/{soundFileId:Guid}/skipped")]
        public ActionResult Skipped([FromRoute] Guid soundFileId)
        {
            _libraryService.FinishedSoundFile(soundFileId, false);
            return Ok();
        }
    }
}
