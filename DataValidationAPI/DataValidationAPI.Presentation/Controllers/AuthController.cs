using Microsoft.AspNetCore.Mvc;

namespace DataValidationAPI.Presentation.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Зарегистироваться
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync()
        {
            return Ok();
        }

        /// <summary>
        /// Войти
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync()
        {
            return Ok();
        }

        /// <summary>
        /// Выйти
        /// </summary>
        [HttpGet("logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            return Ok();
        }

        /// <summary>
        /// Перезайти
        /// </summary>
        [HttpGet("refresh")]
        public async Task<IActionResult> RefreshAsync()
        {
            return Ok();
        }
    }
}
