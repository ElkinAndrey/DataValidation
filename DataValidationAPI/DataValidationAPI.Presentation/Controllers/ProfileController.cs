using DataValidationAPI.Domain.Constants;
using DataValidationAPI.Infrastructure.Dto.Profile;
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
        [Route("change")]
        [Authorize]
        public async Task<IActionResult> ChangeYourProfileAsync(ChangeYourProfileDto record)
        {
            return Ok();
        }

        [HttpPost]
        [Route("delete")]
        [Authorize]
        public async Task<IActionResult> DeleteYourAccountAsync()
        {
            return Ok();
        }
    }
}
