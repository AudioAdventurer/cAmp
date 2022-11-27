using System;
using System.IO;
using cAmp.Libraries.Common.Interfaces;
using cAmp.Libraries.Common.Objects;

namespace cAmp.Libraries.Common.Services
{
    public class FileService
    {
        private readonly Library _libary;
        private readonly IcAmpLogger _logger;

        public FileService(
            Library library,
            IcAmpLogger logger)
        {
            _libary = library;
            _logger = logger;
        }

        public Stream GetFile(Guid soundFileId)
        {
            if (_libary.ContainsSoundFile(soundFileId))
            {
                var soundFile = _libary.GetSoundFile(soundFileId);
                return GetFile(soundFile.Filename);
            }

            return null;
        }

        public Stream GetFile(string filename)
        {
            return File.OpenRead(filename);
        }
    }
}
