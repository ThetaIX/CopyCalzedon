using Microsoft.AspNetCore.Mvc;

namespace CalzedoniaHRFeed.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "API работает!";
        }
    }
}