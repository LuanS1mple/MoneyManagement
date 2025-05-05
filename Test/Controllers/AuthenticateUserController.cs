using ClientAuthentication;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Test.Controllers
{

    [Route("api/[controller]")]
    public class AuthenticateUserController : Controller
    {
        private readonly IAuthenticationService authenticationService;
        private readonly IConfiguration configuration;
        public AuthenticateUserController(IAuthenticationService authenticationService, IConfiguration configuration) {
            this.authenticationService = authenticationService;
            this.configuration = configuration;
        }
        [HttpGet("token")]
        [Authorize]
        public IActionResult Token()
        {
            string key = configuration.GetSection("IssuerSigningKey").Value;
            string token = HttpContext.Request.Headers["RefreshToken"];
            int time = int.Parse(configuration.GetSection("TimeRefresh")!.Value);
            string newRefreshToken = authenticationService.CreateNewRefreshToken(time, token);
            Customer customer = authenticationService.GetByRefreshToken(token);
            string newAccessToken = authenticationService.GetAccessTokenByCustomer(customer,key);
            var result = new
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
            };
            return Json(result);
        }
    }

}
