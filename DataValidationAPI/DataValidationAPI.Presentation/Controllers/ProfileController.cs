using DataValidationAPI.Domain.Constants;
using DataValidationAPI.Infrastructure.Dto.User;
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

        [HttpPut]
        [Route("email")]
        [Authorize]
        public async Task<IActionResult> ChangeEmailAsync(ChangeEmailDto record)
        {
            var user = await Tokens.GetPersonByToken(this);

            if (user is null)
                throw new AccountWasNotLoggedInCorrectlyException();

            if (record.NewEmail is null)
                throw new NewEmailNotSpecifiedException();

            await _userService.ChangeEmailAsync(user.Id, record.NewEmail);

            Response.Cookies.Delete("refreshToken");

            return Ok();
        }

        [HttpPut]
        [Route("password")]
        [Authorize]
        public async Task<IActionResult> ChangePasswordAsync(ChangePasswordDto record)
        {
            var user = await Tokens.GetPersonByToken(this);

            if (user is null)
                throw new AccountWasNotLoggedInCorrectlyException();

            if (record.NewPassword is null)
                throw new NewPasswordNotSpecifiedException();

            await _userService.ChangePasswordAsync(user.Id, record.NewPassword);
            
            Response.Cookies.Delete("refreshToken");

            return Ok();
        }
    }
}
