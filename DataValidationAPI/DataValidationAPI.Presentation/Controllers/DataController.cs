using DataValidationAPI.Domain.Constants;
using DataValidationAPI.Domain.Entities;
using DataValidationAPI.Infrastructure.Dto.Data;
using DataValidationAPI.Infrastructure.Exceptions;
using DataValidationAPI.Presentation.Features;
using DataValidationAPI.Service.Abstractions;
using DataValidationAPI.Service.Dto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DataValidationAPI.Presentation.Controllers
{
    /// <summary>
    /// Контроллер для работы с данными
    /// </summary>
    [Route("api/data")]
    [ApiController]
    public class DataController : ControllerBase
    {
        /// <summary>
        /// Сервис данных
        /// </summary>
        private IDataService _service;

        /// <summary>
        /// Контроллер для работы с данными
        /// </summary>
        /// <param name="service">Сервис данных</param>
        public DataController(IDataService service)
        {
            _service = service;
        }

        /// <summary>
        /// Указать, что данные прошли проверку
        /// </summary>
        [HttpPost]
        [Route("{dataId}/valid")]
        [Authorize(Policy = Policies.Manager)]
        public async Task<IActionResult> ValidDataAsync(Guid dataId)
        {
            var user = await Tokens.GetPersonByToken(this);

            if (user is null)
                throw new AccountWasNotLoggedInCorrectlyException();

            await _service.RateDataAsync(dataId, user.Id, true);

            return Ok();
        }

        /// <summary>
        /// Указать, что данные не прошли проверку
        /// </summary>
        [HttpPost]
        [Route("{dataId}/invalid")]
        [Authorize(Policy = Policies.Manager)]
        public async Task<IActionResult> InvalidDataAsync(Guid dataId)
        {
            var user = await Tokens.GetPersonByToken(this);

            if (user is null)
                throw new AccountWasNotLoggedInCorrectlyException();

            await _service.RateDataAsync(dataId, user.Id, false);

            return Ok();
        }
        
        /// <summary>
        /// Указать, что данные пока проверяются
        /// </summary>
        [HttpPost]
        [Route("{dataId}/review")]
        [Authorize(Policy = Policies.Manager)]
        public async Task<IActionResult> ReviewDataAsync(Guid dataId)
        {
            var user = await Tokens.GetPersonByToken(this);

            if (user is null)
                throw new AccountWasNotLoggedInCorrectlyException();

            await _service.RateDataAsync(dataId, user.Id, null);

            return Ok();
        }

        /// <summary>
        /// Добавить не провереннные данные
        /// </summary>
        [HttpPost]
        [Route("add")]
        [Authorize]
        public async Task<IActionResult> AddNoValidDatesAsync(AddNoValidDatesDto record)
        {
            var user = await Tokens.GetPersonByToken(this);
            await _service.AddNoValidDatesAsync(
                userId: user!.Id,
                information: record.Information);

            return Ok();
        }

        /// <summary>
        /// Получить список данных
        /// </summary>
        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDatasAsync(GetDatasDto record)
        {
            var user = await Tokens.GetPersonByToken(this);

            var datas = await _service.GetDatasAsync(new GetDataParams()
            {
                Start = record.Start ?? 0,
                Length = record.Length ?? int.MaxValue,
                Email = record.Email,
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
                    TakeOnlyThisUser = false,
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
        /// Получить список данных
        /// </summary>
        [HttpPost]
        [Route("count")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDataCountAsync(GetDataCountDto record)
        {
            var user = await Tokens.GetPersonByToken(this);

            int count = await _service.GetDataCountAsync(new GetDataCountParams()
            {
                Email = record.Email,
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
                    TakeOnlyThisUser = false,
                }
            });

            return Ok(count);
        }

        /// <summary>
        /// Получить данные по Id
        /// </summary>
        [HttpGet]
        [Route("{dataId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDataByIdAsync(Guid dataId)
        {
            var user = await Tokens.GetPersonByToken(this);
            var data = await _service.GetDataByIdAsync(dataId, user);

            return Ok(new
            {
                data.Id,
                data.Date,
                data.Information,
                PersonProvided = data.PersonProvided is null ? null : new
                {
                    data.PersonProvided.Id,
                    data.PersonProvided.Email,
                    role = data.PersonProvided.Role?.Name!
                },
                DataChecks = data.DataCheck is null ? null : new
                {
                    PersonChecking = data.DataCheck.User is null ? null : new
                    {
                        data.DataCheck.User.Id,
                        data.DataCheck.User.Email,
                        role = data.DataCheck.User.Role?.Name!
                    },
                    data.DataCheck.Valid,
                }
            });
        }

        /// <summary>
        /// Изменить данные
        /// </summary>
        [HttpPut]
        [Route("{dataId}/change")]
        [Authorize(Policy = Policies.Admin)]
        public async Task<IActionResult> ChangeDataAsync(Guid dataId, ChangeDataDto record)
        {
            await _service.ChangeDataAsync(
                dataId,
                record.Information,
                record.PersonProvidedId);

            return Ok();
        }

        /// <summary>
        /// Удалить данные
        /// </summary>
        [HttpDelete]
        [Route("{dataId}/delete")]
        [Authorize(Policy = Policies.Admin)]
        public async Task<IActionResult> DeleteDataAsync(Guid dataId)
        {
            await _service.DeleteDataAsync(dataId);

            return Ok();
        }
    }
}