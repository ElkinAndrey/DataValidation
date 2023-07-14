using DataValidationAPI.Service.Abstractions;
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
        [Route("")]
        public async Task<IActionResult> AddNoValidDatesAsync()
        {
            return Ok();
        }
    }
}