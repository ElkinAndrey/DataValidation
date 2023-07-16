using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DataValidationAPI.Presentation.Controllers
{
    [Route("api/role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        public RoleController() { }

        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRolesAsync()
        {
            return Ok();
        }

        [HttpGet]
        [Route("{roleId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRoleByIdAsync(Guid roleId)
        {
            return Ok();
        }
    }
}
