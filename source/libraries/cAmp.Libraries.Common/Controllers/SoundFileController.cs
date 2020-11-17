using System;
using System.IO;
using cAmp.Libraries.Common.Interfaces;
using cAmp.Libraries.Common.Services;
using Microsoft.AspNetCore.Mvc;

namespace cAmp.Libraries.Common.Controllers
{
    public class SoundFileController : ControllerBase
    {
        private FileService _fileService;
        private IcAmpLogger _logger;
        
        public SoundFileController(
            FileService fileService,
            IcAmpLogger logger)
        {
            _fileService = fileService;
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
    }
}
