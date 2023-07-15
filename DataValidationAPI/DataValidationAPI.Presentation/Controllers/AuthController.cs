using DataValidationAPI.Domain.Constants;
using DataValidationAPI.Infrastructure.Dto.Auth;
using DataValidationAPI.Presentation.Exceptions;
using DataValidationAPI.Service.Abstractions;
using Microsoft.AspNetCore.Mvc;

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
            // Проверить, есть ли токен в куки
            string? refreshToken = Request.Cookies["refreshToken"]; // Токен обновления
            if (refreshToken is null)
                return Ok();

            // Удалить токен обновления из куков
            Response.Cookies.Delete("refreshToken");

            // Удалить токен из базы дынных
            await _authService.DeleteTokenAsync(refreshToken);

            return Ok();
        }

        /// <summary>
        /// Перезайти
        /// </summary>
        [HttpGet("refresh")]
        public async Task<IActionResult> RefreshAsync()
        {            
            // Достать токен обновления из куки
            string? refreshToken = Request.Cookies["refreshToken"];

            // Если токена нет, то выдать ошибку
            if (string.IsNullOrEmpty(refreshToken))
                throw new RefreshTokenNotInCookieException();

            var tokens = await _authService.RefreshTokenAsync(
                token: refreshToken!,
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
    }
}
