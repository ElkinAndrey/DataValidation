using DataValidationAPI.Infrastructure.Dto.ProfileData;
using DataValidationAPI.Presentation.Exceptions;
using DataValidationAPI.Presentation.Features;
using DataValidationAPI.Service.Abstractions;
using DataValidationAPI.Service.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DataValidationAPI.Presentation.Controllers
{
    /// <summary>
    /// Контроллер для работы с данными через профиль пользователя
    /// </summary>
    [Route("api/profile/data")]
    [ApiController]
    public class ProfileDataController : ControllerBase
    {
        /// <summary>
        /// Сервис для работы с данными
        /// </summary>
        private IDataService _service;

        /// <summary>
        /// Контроллер для работы с данными через профиль пользователя
        /// </summary>
        /// <param name="service"></param>
        public ProfileDataController(IDataService service)
        {
            _service = service;
        }

        /// <summary>
        /// Получить список своих данных
        /// </summary>
        [HttpPost]
        [Route("")]
        [Authorize]
        public async Task<IActionResult> GetYourDataListAsync(GetYourDataDto record)
        {
            var user = await Tokens.GetPersonByToken(this);

            var datas = await _service.GetDatasAsync(new GetDataParams()
            {
                Start = record.Start ?? 0,
                Length = record.Length ?? int.MaxValue,
                DateStart = record.DateStart,
                DateEnd = record.DateEnd,
                IsUnverifiedData = record.IsUnverifiedData ?? true,
                IsValidatedData = record.IsValidatedData ?? true,
                IsNoValidatedData = record.IsNoValidatedData ?? true,
                IsCheckData = record.IsCheckData ?? true,
                UserParam = user is null ? null : new UserParam()
                {
                    UserId = user.Id,
                    RecipientDataId = user.Id,
                    RoleRecipientData = user.Role?.Name!,
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
        /// Удалить свои данные
        /// </summary>
        [HttpDelete]
        [Route("{dataId}/delete")]
        [Authorize]
        public async Task<IActionResult> DeleteYourDataAsync(Guid dataId)
        {
            var user = await Tokens.GetPersonByToken(this);
            if (user is null)
                throw new AccountWasNotLoggedInCorrectlyException();

            await _service.DeleteDataAsync(dataId, user.Id);

            return Ok();
        }

        /// <summary>
        /// Изменить свои данные
        /// </summary>
        [HttpPut]
        [Route("{dataId}/change")]
        [Authorize]
        public async Task<IActionResult> ChangeYourDataAsync(Guid dataId, ChangeYourDataDto record)
        {
            var user = await Tokens.GetPersonByToken(this);
            if (user is null)
                throw new AccountWasNotLoggedInCorrectlyException();

            await _service.ChangeDataAsync(
                id: dataId,
                information: record.Information,
                userId: user.Id);

            return Ok();
        }
    }
}
