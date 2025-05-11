using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MM.HostApp.Models;
using MM.Usecase;

namespace MM.HostApp.Controllers.UsageManagement
{
    [Route("usage")]
    public class UsageController : Controller
    {
        private readonly IUsageRepository _usageRepository;
        private readonly ITypeUsageRepository _typeUsageRepository;
        public UsageController(IUsageRepository usageRepository, ITypeUsageRepository typeUsageRepository)
        {
            _typeUsageRepository = typeUsageRepository;
            _usageRepository = usageRepository;
        }
        [Authorize]
        [HttpGet("usages")]
        public IActionResult Display()
        {
            var usages = _usageRepository.GetAll();
            var response = new UsageResponse() {usages = usages, types= _typeUsageRepository.GetAllTypes() };
            return View("AllUsage",response);
        }
        [Authorize]
        [HttpPost("add")]
        public IActionResult Add([FromForm] RequestUsage request)
        {
            Usage usage = new Usage() {Name = request.Name, TypeId= request.TypeId, Enable=true };
            _usageRepository.AddUsage(usage);
            return RedirectToAction("usages");
        }
        [Authorize]
        [HttpGet("delete")]
        public IActionResult Delete([FromQuery] int usageId)
        {
            var usage = _usageRepository.GetById(usageId);
            _usageRepository.Delete(usage);
            return RedirectToAction("usages");
        }
    }
}
