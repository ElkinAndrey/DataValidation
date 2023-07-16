using DataValidationAPI.Domain.Entities;
using DataValidationAPI.Presentation.Controllers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DataValidationAPI.Presentation.Features
{
    public static class Tokens
    {
        /// <summary>
        /// Получить пользователя из токена
        /// </summary>
        /// <returns>Пользователь</returns>
        public static async Task<User?> GetPersonByToken(ControllerBase controller)
        {
            var accessToken = await controller.HttpContext.GetTokenAsync("access_token");
            if (accessToken is null)
                return null;
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(accessToken);
            var user = GetPersonByClaim(controller, jwtSecurityToken.Claims);
            return user;
        }

        /// <summary>
        /// Получить человека из клаймов
        /// </summary>
        /// <param name="claims">Клаймы</param>
        /// <returns>Пользователь</returns>
        private static User GetPersonByClaim(ControllerBase controller, IEnumerable<Claim> claims)
        {
            var user = new User() { Role = new Role() };
            foreach (var claim in claims)
            {
                if (claim.Type == ClaimTypes.NameIdentifier)
                    user.Id = new Guid(claim.Value);
                else if (claim.Type == ClaimTypes.Email)
                    user.Email = claim.Value;
                else if (claim.Type == ClaimTypes.Role)
                    user.Role.Name = claim.Value;
            }
            return user;
        }
    }
}
