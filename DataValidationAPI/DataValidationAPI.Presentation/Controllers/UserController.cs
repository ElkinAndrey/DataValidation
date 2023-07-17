using DataValidationAPI.Domain.Constants;
using DataValidationAPI.Infrastructure.Dto.User;
using DataValidationAPI.Presentation.Features;
using DataValidationAPI.Service.Abstractions;
using DataValidationAPI.Service.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DataValidationAPI.Presentation.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IDataService _dataService;
        private IUserService _userService;

        public UserController(IDataService dataService, IUserService userService)
        {
            _dataService = dataService;
            _userService = userService;
        }

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
        
        [HttpDelete]
        [Route("{userId}/delete")]
        [Authorize(Policy = Policies.Admin)]
        public async Task<IActionResult> DeleteUserAsync(Guid userId)
        {
            await _userService.DeleteUserAsync(userId);

            return Ok();
        }
    }
}
