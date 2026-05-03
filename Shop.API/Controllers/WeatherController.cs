using Microsoft.AspNetCore.Mvc;

namespace Shop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok("blogas oras");
        }
    }
}
