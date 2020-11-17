using cAmp.Libraries.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace cAmp.Libraries.Common.Controllers
{
    [Produces("text/plain")]
    [Route("/api")]
    public class StatusController : ControllerBase
    {
        private readonly IcAmpLogger _logger;

        public StatusController(
            IcAmpLogger logger)
        {
            _logger = logger;
        }

        public string Get()
        {
            _logger.Info("cAmp Media Player Status");
            return "cAmp Media Server";
        }
    }
}
