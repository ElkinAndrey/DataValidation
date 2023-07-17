using DataValidationAPI.Presentation.Exceptions;
using DataValidationAPI.Presentation.Features;
using DataValidationAPI.Service.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DataValidationAPI.Presentation.Controllers
{
    [Route("api/profile")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private IUserService _userService;

        public ProfileController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("")]
        [Authorize]
        public async Task<IActionResult> GetYourProfileAsync()
        {
            var userFromToken = await Tokens.GetPersonByToken(this);

            if (userFromToken is null)
                throw new AccountWasNotLoggedInCorrectlyException();

            var user = await _userService.GetUserByIdAsync(userFromToken.Id);

            return Ok(new
            {
                user.Id,
                user.Email,
                Role = user.Role?.Name!,
                user.IsActive,
                user.RegistrationDate,
            });
        }

        [HttpPost]
        [Route("block")]
        [Authorize]
        public async Task<IActionResult> BlockYourAccountAsync()
        {
            var user = await Tokens.GetPersonByToken(this);

            if (user is null)
                throw new AccountWasNotLoggedInCorrectlyException();

            await _userService.ChangeBlockUserAsync(user.Id, false);

            return Ok();
        }
    }
}
