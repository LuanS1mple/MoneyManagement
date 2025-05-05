using ClientAuthentication;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using MM.HostApp.Models;
using MM.HostApp.RemoteAuthencation;
using MM.Usecase;
using System.Diagnostics;

namespace MM.HostApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICustomerRepository _customerRepository;
        private readonly IAuthenticationService authenticationService;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, ICustomerRepository customerRepository, IAuthenticationService authenticationService, IConfiguration configuration)
        {
            _logger = logger;
            _customerRepository = customerRepository;
            this.authenticationService = authenticationService;
            this._configuration = configuration;
        }
        public async Task<IActionResult> Loggin()
        {
            string accessToken = HttpContext.Request.Headers["AccessToken"];
            string refreshToken = HttpContext.Request.Headers["RefreshToken"];
            if(accessToken == null || refreshToken == null)
            {
                return View("Login");
            }
            if (authenticationService.IsTokenValid(accessToken, _configuration.GetSection("IssuerSigningKey").Value,"LuanS1mple"))
            {
                return Content("OK AccessToken");
            }
            else
            {
                ResponseAuthentication? isAuthenticated = await RemoteApiAuthentication.IsAuthenticated("https://localhost:7242/", refreshToken);
                if(isAuthenticated == null)
                {
                    return Content("Denied RefreshToken");
                }
                else
                {
                    HttpContext.Response.Cookies.Append("AccessToken", isAuthenticated.AccessToken);
                    HttpContext.Response.Cookies.Append("RefreshToken", isAuthenticated.RefreshToken);
                    return Content("OK AccessToken");
                }

            }
        }
        [HttpPost]
        public IActionResult Check([FromForm] string username, [FromForm] string password)
        {
            if (_customerRepository.isCorrectAccount(username, password))
            {

                Customer customer =_customerRepository.GetByAccount(username);
            }
            return Content("Not correct account");
        }
     

      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
