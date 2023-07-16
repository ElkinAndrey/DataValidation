using DataValidationAPI.Persistence.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DataValidationAPI.Presentation.Controllers
{
    [Route("api/role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private IRoleRepository _repository;

        public RoleController(IRoleRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRolesAsync()
        {
            var roles = await _repository.Get();

            return Ok(roles.Select(r => new
            {
                r.Id,
                r.Name,
            }));
        }

        [HttpGet]
        [Route("{roleId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRoleByIdAsync(Guid roleId)
        {
            var role = await _repository.GetById(roleId);

            return Ok(new
            {
                role.Id,
                role.Name,
            });
        }
    }
}
