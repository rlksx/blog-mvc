using Microsoft.AspNetCore.Mvc;

namespace BlogMvc.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult Get()
            => Ok();
    }
}

