using DataValidationAPI.Domain.Constants;
using DataValidationAPI.Infrastructure.Dto.User;
using DataValidationAPI.Presentation.Exceptions;
using DataValidationAPI.Presentation.Features;
using DataValidationAPI.Service.Abstractions;
using DataValidationAPI.Service.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DataValidationAPI.Presentation.Controllers
{
    /// <summary>
    /// Контроллер для работы с пользователями
    /// </summary>
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// Сервис для работы с данными
        /// </summary>
        private IDataService _dataService;

        /// <summary>
        /// Сервис для работы с пользователями
        /// </summary>
        private IUserService _userService;

        /// <summary>
        /// Контроллер для работы с пользователями
        /// </summary>
        /// <param name="dataService">Сервис для работы с данными</param>
        /// <param name="userService">Контроллер для работы с пользователями</param>
        public UserController(IDataService dataService, IUserService userService)
        {
            _dataService = dataService;
            _userService = userService;
        }

        /// <summary>
        /// Получить данные по Id пользователя
        /// </summary>
        [HttpPost]
        [Route("{userId}/data")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDataByUserIdAsync(Guid userId, GetDataByUserIdDto record)
        {
            var user = await Tokens.GetPersonByToken(this);

            var datas = await _dataService.GetDatasAsync(new GetDataParams()
            {
                Start = record.Start ?? 0,
                Length = record.Length ?? int.MaxValue,
                DateStart = record.DateStart,
                DateEnd = record.DateEnd,
                IsUnverifiedData = record.IsUnverifiedData ?? true,
                IsValidatedData = record.IsValidatedData ?? true,
                IsNoValidatedData = record.IsNoValidatedData ?? true,
                IsCheckData = record.IsCheckData ?? true,
                UserParam = new UserParam()
                {
                    UserId = userId,
                    RecipientDataId = user?.Id ?? Guid.Empty,
                    RoleRecipientData = user?.Role?.Name,
                    TakeOnlyThisUser = true,
                }
            });

            return Ok(datas.Select(d => new
            {
                d.Id,
                d.Date,
                d.Information,
                PersonProvided = d.PersonProvided is null ? null : new
                {
                    d.PersonProvided.Id,
                    d.PersonProvided.Email,
                    role = d.PersonProvided.Role?.Name!
                },
                DataChecks = d.DataCheck is null ? null : new
                {
                    PersonChecking = d.DataCheck.User is null ? null : new
                    {
                        d.DataCheck.User.Id,
                        d.DataCheck.User.Email,
                        role = d.DataCheck.User.Role?.Name!
                    },
                    d.DataCheck.Valid,
                }
            }));
        }

        /// <summary>
        /// Получить список пользователей
        /// </summary>
        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUsersAsync(GetUsersDto record)
        {
            var users = await _userService.GetUsersAsync(
                start: record.Start ?? 0,
                length: record.Length ?? int.MaxValue,
                email: record.Email,
                roleId: record.RoleId,
                startRegistrationDate: record.StartRegistrationDate,
                endRegistrationDate: record.EndRegistrationDate);

            return Ok(users.Select(u => new
            {
                u.Id,
                u.Email,
                Role = u.Role?.Name!,
                u.IsActive,
                u.RegistrationDate,
            }));
        }
        
        /// <summary>
        /// Получить пользователя по Id
        /// </summary>
        [HttpGet]
        [Route("{userId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserByIdAsync(Guid userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);

            return Ok(new
            {
                user.Id,
                user.Email,
                Role = user.Role?.Name!,
                user.IsActive,
                user.RegistrationDate,
            });
        }

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        [HttpDelete]
        [Route("{userId}/delete")]
        [Authorize(Policy = Policies.Admin)]
        public async Task<IActionResult> DeleteUserAsync(Guid userId)
        {
            await _userService.DeleteUserAsync(userId);

            return Ok();
        }

        /// <summary>
        /// Заблокировать пользователя
        /// </summary>
        [HttpPut]
        [Route("{userId}/change/block")]
        [Authorize(Policy = Policies.Admin)]
        public async Task<IActionResult> BlockUserAsync(Guid userId)
        {
            await _userService.ChangeBlockUserAsync(userId, false);

            return Ok();
        }

        /// <summary>
        /// Разблокировать пользователя
        /// </summary>
        [HttpPut]
        [Route("{userId}/change/unblock")]
        [Authorize(Policy = Policies.Admin)]
        public async Task<IActionResult> UnblockUserAsync(Guid userId)
        {
            await _userService.ChangeBlockUserAsync(userId, true);

            return Ok();
        }

        /// <summary>
        /// Поменять электронную почту пользователя
        /// </summary>
        [HttpPut]
        [Route("{userId}/change/email")]
        [Authorize(Policy = Policies.Admin)]
        public async Task<IActionResult> ChangeEmailAsync(Guid userId, ChangeEmailDto record)
        {
            if (record.NewEmail is null)
                throw new NewEmailNotSpecifiedException();

            await _userService.ChangeEmailAsync(userId, record.NewEmail);

            return Ok();
        }

        /// <summary>
        /// Поменять пароль пользователя
        /// </summary>
        [HttpPut]
        [Route("{userId}/change/password")]
        [Authorize(Policy = Policies.Admin)]
        public async Task<IActionResult> ChangePasswordAsync(Guid userId, ChangePasswordDto record)
        {
            if (record.NewPassword is null)
                throw new NewPasswordNotSpecifiedException();

            await _userService.ChangePasswordAsync(userId, record.NewPassword);

            return Ok();
        }

        /// <summary>
        /// Выдат пользователю роль менеджера
        /// </summary>
        [HttpPut]
        [Route("{userId}/change/manager/give")]
        [Authorize(Policy = Policies.Admin)]
        public async Task<IActionResult> GiveManagerAsync(Guid userId)
        {
            await _userService.ChangeManagerRoleAsync(userId, true);

            return Ok();
        }
        
        /// <summary>
        /// Забрать у пользователя роль менеджера
        /// </summary>
        [HttpPut]
        [Route("{userId}/change/manager/take")]
        [Authorize(Policy = Policies.Admin)]
        public async Task<IActionResult> TakeManagerAsync(Guid userId)
        {
            await _userService.ChangeManagerRoleAsync(userId, false);

            return Ok();
        }
    }
}
