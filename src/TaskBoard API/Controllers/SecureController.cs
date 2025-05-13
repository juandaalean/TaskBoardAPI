using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TaskBoard_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecureController : ControllerBase
    {
        [HttpGet("secret")]
        [Authorize]
        public IActionResult GetSecret()
        {
            return Ok("Access complete to protected resource");
        }
    }

}
