using Application.Dtos.User;
using Application.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace TaskBoard_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ApiController
    {
        private readonly IAuthService _auth;

        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto request)
        {
            var result = await _auth.RegisterAsync(request);

            return result.Match(
                value => Ok(value),
                errors => Problem(errors)
            );
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto request)
        {
            var result = await _auth.LoginAsync(request);

            return result.Match(
                token => Ok(new { token }),
                errors => Problem(errors)
            );
        }
    }
}
