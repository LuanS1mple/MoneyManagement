using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MM.AuthenticationApi.Controllers
{
    [Route("api/authenticate")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        [Authorize]
        [Route("token")]
        public IActionResult Authenticate()
        {
            return Ok();
        }
    }
}
