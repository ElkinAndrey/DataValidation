using Azure.Core;
using DataValidationAPI.Domain.Constants;
using DataValidationAPI.Infrastructure.Dto.Auth;
using DataValidationAPI.Service.Abstractions;
using DataValidationAPI.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace DataValidationAPI.Presentation.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;

        public AuthController(IConfiguration configuration, IAuthService authService)
        {
            _configuration = configuration;
            _authService = authService;
        }

        /// <summary>
        /// Зарегистироваться
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterDto request)
        {
            var tokens = await _authService.RegisterAsync(
                email: request.Email,
                password: request.Password,
                secretKey: _configuration.GetSection("AppSettings:Token").Value!);

            // Токен обновления записывается в куки
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true, // Куки можно будет изменить только при помощи бекенда, а не при помощи JS
                Expires = tokens.GenerationDate + JwtLifetime.RefreshTimeSpan, // До какого числа будет жить токен
            };
            Response.Cookies.Append("refreshToken", tokens.RefreshToken, cookieOptions);

            return Ok(tokens.AccessToken);
        }

        /// <summary>
        /// Войти
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginDto request)
        {
            var tokens = await _authService.LoginAsync(
                email: request.Email,
                password: request.Password,
                secretKey: _configuration.GetSection("AppSettings:Token").Value!);

            // Токен обновления записывается в куки
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true, // Куки можно будет изменить только при помощи бекенда, а не при помощи JS
                Expires = tokens.GenerationDate + JwtLifetime.RefreshTimeSpan, // До какого числа будет жить токен
            };
            Response.Cookies.Append("refreshToken", tokens.RefreshToken, cookieOptions);

            return Ok(tokens.AccessToken);
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
