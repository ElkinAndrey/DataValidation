using DataValidationAPI.Infrastructure.Dto.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DataValidationAPI.Presentation.Controllers
{
    [Route("api/profile/data")]
    [ApiController]
    public class UserDataController : ControllerBase
    {
        [HttpPost]
        [Route("")]
        [Authorize]
        public async Task<IActionResult> GetYourDataListAsync(GetYourDataDto record)
        {
            return Ok();
        }

        [HttpGet]
        [Route("{dataId}")]
        [Authorize]
        public async Task<IActionResult> GetYourDataByIdAsync(Guid dataId)
        {
            return Ok();
        }

        [HttpDelete]
        [Route("{dataId}/delete")]
        [Authorize]
        public async Task<IActionResult> DeleteYourDataAsync(Guid dataId)
        {
            return Ok();
        }

        [HttpPut]
        [Route("{dataId}/change")]
        [Authorize]
        public async Task<IActionResult> ChangeYourDataAsync(Guid dataId, ChangeYourDataDto record)
        {
            return Ok();
        }
    }
}
