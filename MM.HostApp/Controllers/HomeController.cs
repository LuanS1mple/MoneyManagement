using ClientAuthentication;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
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
                return View("Login");
        }
        [HttpPost]
        public IActionResult Check([FromForm] string username, [FromForm] string password)
        {
            if (_customerRepository.isCorrectAccount(username, password))
            {

                Customer customer = _customerRepository.GetByAccount(username);
                int time = int.Parse(_configuration.GetSection("TimeRefresh").Value);
                string refreshToken = authenticationService.CreateNewRefreshToken(time, customer);
                string accessToken = authenticationService.GetAccessTokenByCustomer(customer, _configuration.GetSection("IssuerSigningKey").Value);
                HttpContext.Response.Cookies.Append("AccessToken", accessToken);
                HttpContext.Response.Cookies.Append("RefreshToken", refreshToken);
                return View("MainScreen");
            }
            return Content("Not correct account");
        }
        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
