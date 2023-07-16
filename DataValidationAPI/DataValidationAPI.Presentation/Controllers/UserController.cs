﻿using Azure.Core;
using DataValidationAPI.Domain.Constants;
using DataValidationAPI.Infrastructure.Dto.User;
using DataValidationAPI.Presentation.Features;
using DataValidationAPI.Service.Abstractions;
using DataValidationAPI.Service.Dto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using System.Text.Json;

namespace DataValidationAPI.Presentation.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IDataService _service;

        public UserController(IDataService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("{userId}/data")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDataByUserIdAsync(Guid userId, GetDataByUserIdDto record)
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
            return Ok();
        }
        
        [HttpGet]
        [Route("{userId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserByIdAsync(Guid userId)
        {
            return Ok();
        }
        
        [HttpPut]
        [Route("{userId}/change")]
        [Authorize(Policy = Policies.Admin)]
        public async Task<IActionResult> ChangeUserAsync(Guid userId, ChangeUserDto record)
        {
            return Ok();
        }
        
        [HttpDelete]
        [Route("{userId}/delete")]
        [Authorize(Policy = Policies.Admin)]
        public async Task<IActionResult> DeleteUserAsync(Guid userId)
        {
            return Ok();
        }
    }
}
