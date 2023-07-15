using DataValidationAPI.Domain.Constants;
using DataValidationAPI.Domain.Entities;
using DataValidationAPI.Infrastructure.Dto.Data;
using DataValidationAPI.Service.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DataValidationAPI.Presentation.Controllers
{
    [Route("api/data")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private IDataService _service;

        public DataController(IDataService service)
        {
            _service = service;

        }

        [HttpPost]
        [Route("add")]
        [Authorize]
        public async Task<IActionResult> AddNoValidDatesAsync(AddNoValidDatesDto record)
        {
            var user = await GetPersonByToken();
            await _service.AddNoValidDatesAsync(
                userId: user.Id,
                information: record.Information);

            return Ok();
        }

        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDatasAsync(GetDatasDto record)
        {
            var user = await GetPersonByToken();

            var onlyValid = true;
            var userId = (Guid?)null;

            switch (user?.Role?.Name!)
            {
                default:
                    onlyValid = true;
                    userId = null;
                    break;
                case Roles.User:
                    userId = user.Id;
                    onlyValid = true;
                    break;
                case Roles.Manager:
                case Roles.Admin:
                    onlyValid = false;
                    userId = null;
                    break;
            }

            var datas = await _service.GetDatasAsync(
                start: record.Start ?? 0,
                length: record.Length ?? int.MaxValue,
                onlyValid: onlyValid,
                userId: userId,
                email: record.Email,
                dateStart: record.DateStart,
                dateEnd: record.DateEnd);

            return Ok(datas.Select(d => new
            {
                d.Id,
                d.Date,
                d.Information,
                PersonProvided = new
                {
                    d.PersonProvided.Id,
                    d.PersonProvided.Email,
                    role = d.PersonProvided.Role.Name
                },
                DataChecks = d.DataCheck is null ? null : new
                {
                    PersonChecking = new
                    {
                        d.DataCheck.User.Id,
                        d.DataCheck.User.Email,
                        role = d.DataCheck.User.Role.Name
                    },
                    d.DataCheck.Valid,
                }
            }));
        }

        [HttpGet]
        [Route("{dataId}")]
        [Authorize]
        public async Task<IActionResult> GetDataByIdAsync(Guid dataId)
        {
            return Ok();
        }

        [HttpPut]
        [Route("{dataId}/change")]
        [Authorize(Policy = Policies.Admin)]
        public async Task<IActionResult> ChangeDataAsync(Guid dataId, ChangeDataDto record)
        {
            return Ok();
        }

        [HttpDelete]
        [Route("{dataId}/delete")]
        [Authorize(Policy = Policies.Admin)]
        public async Task<IActionResult> DeleteDataAsync(Guid dataId)
        {
            return Ok();
        }

        #region Вспомогательные функции

        /// <summary>
        /// Получить пользователя из токена
        /// </summary>
        /// <returns>Пользователь</returns>
        private async Task<User?> GetPersonByToken()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            if (accessToken is null)
                return null;
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(accessToken);
            var user = GetPersonByClaim(jwtSecurityToken.Claims);
            return user;
        }

        /// <summary>
        /// Получить человека из клаймов
        /// </summary>
        /// <param name="claims">Клаймы</param>
        /// <returns>Пользователь</returns>
        private User GetPersonByClaim(IEnumerable<Claim> claims)
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

        #endregion
    }
}