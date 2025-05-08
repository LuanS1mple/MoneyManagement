using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MM.HostApp.Mapper;
using MM.HostApp.Models;
using MM.HostApp.ResourceProviderRemote;
using MM.Usecase;

namespace MM.HostApp.Controllers.JarManagement
{
    [Route("jar")]
    public class JarController : Controller
    {
        private readonly IJarRepository _jarRepository;
        private readonly ICustomerRepository _customerRepository;
        public JarController(IJarRepository jarRepository, ICustomerRepository customerRepository)
        {
            this._customerRepository = customerRepository;
            _jarRepository = jarRepository;
        }
        [Authorize]
        [HttpGet("jars")]
        public async Task<IActionResult> DisplayJar()
        {
            string customerId_raw = HttpContext.User.FindFirst("aud").Value;
            int customerId = int.Parse(customerId_raw);
            List<ResponseJar> jars = MapperObject.GetResponseJarsFromJars(_jarRepository.GetAll(customerId));
            ResponseJarController responseJarController = new ResponseJarController()
            {
                Message = null,
                ResponseJars = jars
            };
            return View("AllJar",responseJarController);
        }
        [Authorize]
        [HttpPost("add")]
        public IActionResult Add([FromForm] RequestJar requestJar)
        {
            string customerId_raw = HttpContext.User.FindFirst("aud").Value;
            int customerId = int.Parse(customerId_raw);
            Customer customer = _customerRepository.GetById(customerId);
            if(customer.Deposit < requestJar.Total)
            {
                List<ResponseJar> jars = MapperObject.GetResponseJarsFromJars(_jarRepository.GetAll(customerId));
                string errorMessage = "Bạn không đủ tiền để tạo hũ này";
                ResponseJarController responseJarController = new ResponseJarController() {
                    Message = errorMessage,
                    ResponseJars = jars
                };
                return View("AllJar", responseJarController);
            }
            requestJar.CustomerId = customerId;
            Jar newJar = MapperObject.GetJarFromRequestJar(requestJar);
            _customerRepository.AddDeposit(-requestJar.Total, customerId);
            _jarRepository.Add(newJar);
            return RedirectToAction("jars");
        }
        [Authorize]
        [HttpGet("delete")]
        public IActionResult Delete([FromQuery] int jarId)
        {
            string customerId_raw = HttpContext.User.FindFirst("aud").Value;
            int customerId = int.Parse(customerId_raw);
            Jar jar = _jarRepository.GetById(jarId);
            int amonut = jar.Total.Value;
            _customerRepository.AddDeposit(amonut, customerId);
            _jarRepository.Delete(jar);
            return RedirectToAction("jars");
        }
       
    }
}
