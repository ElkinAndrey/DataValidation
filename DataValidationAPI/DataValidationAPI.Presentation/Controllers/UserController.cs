using Azure.Core;
using DataValidationAPI.Domain.Constants;
using DataValidationAPI.Infrastructure.Dto.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using System.Text.Json;

namespace DataValidationAPI.Presentation.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [Route("{userId}/data")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDataByUserIdAsync(Guid userId, GetDataByUserIdDto record)
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
