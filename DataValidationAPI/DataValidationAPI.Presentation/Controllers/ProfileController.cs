using DataValidationAPI.Domain.Constants;
using DataValidationAPI.Infrastructure.Dto.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DataValidationAPI.Presentation.Controllers
{
    [Route("api/profile")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        [Authorize]
        public async Task<IActionResult> GetYourProfileAsync()
        {
            return Ok();
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
