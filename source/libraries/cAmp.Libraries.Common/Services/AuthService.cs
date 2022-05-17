using System;
using cAmp.Libraries.Common.Interfaces;
using cAmp.Libraries.Common.UserInterfaceObjects;

namespace cAmp.Libraries.Common.Services
{
    public class AuthService
    {
        private readonly UserService _userService;
        private readonly IcAmpLogger _logger;

        public AuthService(
            UserService userService,
            IcAmpLogger logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public IWebSession CreateSession(
            string username,
            out Records.User user)
        {
            user = _userService.GetUserByUsername(username);

            if (user != null)
            {
                var webSession = new WebSession
                {
                    Id = Guid.NewGuid(),
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username,
                    UserId = user.Id
                };

                return webSession;
            }

            return null;
        }
    }
}
