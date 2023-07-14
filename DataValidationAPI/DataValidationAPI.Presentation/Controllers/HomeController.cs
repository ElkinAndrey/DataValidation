using DataValidationAPI.Domain.Constants;
using DataValidationAPI.Service.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DataValidationAPI.Presentation.Controllers
{
    [Route("api/home")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private IDataService _service;

        public HomeController(IDataService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("all")]
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            return Ok("all");
        }

        [HttpGet]
        [Route("reg")]
        [Authorize]
        public async Task<IActionResult> Reg()
        {
            return Ok("reg");
        }

        [HttpGet]
        [Route("manag")]
        [Authorize(Policy = Policies.Manager)]
        public async Task<IActionResult> Manag()
        {
            return Ok("manag");
        }

        [HttpGet]
        [Route("admin")]
        [Authorize(Policy = Policies.Admin)]
        public async Task<IActionResult> Admin()
        {
            return Ok("admin");
        }
    }
}