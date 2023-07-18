using DataValidationAPI.Persistence.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DataValidationAPI.Presentation.Controllers
{
    /// <summary>
    /// Контроллер для работы с ролями
    /// </summary>
    [Route("api/role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        /// <summary>
        /// Репозиторий для работы с ролями
        /// </summary>
        private IRoleRepository _repository;

        /// <summary>
        /// Контроллер для работы с ролями
        /// </summary>
        /// <param name="repository">Репозиторий для работы с ролями</param>
        public RoleController(IRoleRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Получить роли
        /// </summary>
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

        /// <summary>
        /// Получить роль по Id
        /// </summary>
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
