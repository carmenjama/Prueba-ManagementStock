using Microsoft.AspNetCore.Mvc;

namespace ManagementTransaction.Api.Controllers
{
    [ApiController]
    [Route("")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("Weather forecast here");
    }
}
