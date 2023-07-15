using DataValidationAPI.Domain.Constants;
using DataValidationAPI.Domain.Entities;
using DataValidationAPI.Infrastructure.Dto.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DataValidationAPI.Presentation.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [Route("{userId}/data")]
        [Authorize]
        public async Task<IActionResult> GetDataByUserIdAsync(Guid userId, GetDataByUserIdDto record)
        {
            return Ok();
        }

        [HttpPost]
        [Route("{userId}/data/only-valid")]
        [AllowAnonymous]
        public async Task<IActionResult> GetOnlyValidDataByUserIdAsync(Guid userId, GetDataByUserIdDto record)
        {
            return Ok();
        }

        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUsersAsync(GetUsersDto record)
        {
            return Ok();
        }
        
        [HttpGet]
        [Route("{userId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserByIdAsync(Guid userId)
        {
            return Ok();
        }
        
        [HttpPut]
        [Route("{userId}/change")]
        [Authorize(Policy = Policies.Admin)]
        public async Task<IActionResult> ChangeUserAsync(Guid userId, ChangeUserDto record)
        {
            return Ok();
        }
        
        [HttpDelete]
        [Route("{userId}/delete")]
        [Authorize(Policy = Policies.Admin)]
        public async Task<IActionResult> DeleteUserAsync(Guid userId)
        {
            return Ok();
        }
    }
}
