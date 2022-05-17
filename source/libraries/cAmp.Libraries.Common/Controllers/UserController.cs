using System;
using System.Collections.Generic;
using cAmp.Libraries.Common.Interfaces;
using cAmp.Libraries.Common.Services;
using cAmp.Libraries.Common.UserInterfaceObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cAmp.Libraries.Common.Controllers
{
    [Authorize]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IcAmpLogger _logger;

        public UserController(
            UserService userService,
            IcAmpLogger logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        [Route("api/users")]
        public ActionResult<List<UserInterfaceObjects.User>> GetUsers()
        {
            var users = _userService.GetUsers();
            return Ok(users);
        }

        [HttpGet]
        [Route("api/users/{userId:Guid}")]
        public ActionResult<UserInterfaceObjects.User> GetUser(
            [FromRoute] Guid userId)
        {
            var user = _userService.GetUser(userId);
            return Ok(user);
        }

        [HttpPost]
        [Route("api/users")]
        public ActionResult SaveUser([FromBody] UserInterfaceObjects.User user)
        {
            _userService.SaveUser(
                user.Id, 
                user.FirstName, 
                user.LastName, 
                user.Username);

            return Ok();
        }

        [HttpPost]
        [Route("api/users/{userId:Guid}/password")]
        public ActionResult SetPassword(
            [FromRoute] Guid userId,
            [FromBody] SetPasswordRequest setPassword)
        {
            _userService.SetPassword(
                userId, 
                setPassword.NewPassword);

            return Ok();
        }

        [HttpPost]
        [Route("api/users/{userId:Guid}/volume/{volume:int}")]
        public ActionResult SetPassword(
            [FromRoute] Guid userId,
            [FromRoute] int volume)
        {
            _userService.SetPassword(
                userId,
                setPassword.NewPassword);

            return Ok();
        }
    }
}
