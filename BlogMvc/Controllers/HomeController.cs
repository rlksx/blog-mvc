using BlogMvc.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace BlogMvc.Controllers
{
    [ApiController]
    [Route("/")]
    [ApiKey]
    public class HomeController : ControllerBase
    {
        [HttpGet("/")]
        public IActionResult Get()
            => Ok();
    }
}

