﻿using System.Collections.Generic;
using cAmp.Libraries.Common.Helpers;
using cAmp.Libraries.Common.Interfaces;
using cAmp.Libraries.Common.Services;
using cAmp.Libraries.Common.UserInterfaceObjects;
using Microsoft.AspNetCore.Mvc;

namespace cAmp.Libraries.Common.Controllers
{
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly JwtService _jwtService;
        private readonly UserService _userService;
        private readonly IcAmpLogger _logger;

        public AuthController(
            AuthService authService,
            JwtService jwtService,
            UserService userService,
            IcAmpLogger logger)
        {
            _authService = authService;
            _jwtService = jwtService;
            _userService = userService;
            _logger = logger;
        }

        [HttpPost]
        [Route("api/login/{username}")]
        public ActionResult<LoginResponse> Login(
            [FromRoute] string username)
        {
            _logger.Info($"POST:api/login/{username}");

            var webSession = _authService.CreateSession(
                username,
                out Records.User user);

            if (webSession != null)
            {
                var jwt = _jwtService.CreateJwt(webSession);

                var response = new LoginResponse
                {
                    JWT = jwt,
                    Volume = user.Volume
                };

                return Ok(response);
            }

            return Unauthorized();
        }

        [HttpGet]
        [Route("api/login/users")]
        public ActionResult<List<UserInterfaceObjects.User>> AvailableUsers()
        {
            _logger.Info("GET:api/login/users");

            var users = _userService.GetUsers();

            return Ok(users.ToUserInterfaceObjects());
        }
    }
}
