using System;
using System.Collections.Generic;
using System.Text;
using cAmp.Libraries.Common.Interfaces;
using cAmp.Libraries.Common.Services;
using Microsoft.AspNetCore.Mvc;

namespace cAmp.Libraries.Common.Controllers
{
    public class GenreController : ControllerBase
    {
        private readonly LibraryService _libraryService;
        private readonly IcAmpLogger _logger;

        public GenreController(
            LibraryService libraryService,
            IcAmpLogger logger)
        {
            _libraryService = libraryService;
            _logger = logger;
        }
    }
}
