using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mİddlewares.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HandleException : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            throw new NotImplementedException("test");
            return Ok("Ok");
        }
    }
}
