using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MM.HostApp.Models;
using MM.Usecase;

namespace MM.HostApp.Controllers.RevenueManagement
{
    [Route("revenue")]
    public class RevenueController : Controller
    {
        private readonly IRevenueRepository revenueRepository;
        private readonly IUsageRepository usageRepository;
        private readonly IJarRepository jarRepository;

        public RevenueController(IRevenueRepository revenueRepository, IUsageRepository usageRepository, IJarRepository jarRepository)
        {
            this.jarRepository = jarRepository;
            this.usageRepository = usageRepository;
            this.revenueRepository = revenueRepository;
        }
        [Authorize]
        [HttpGet("revenues")]
        public IActionResult Display()
        {
            var customerId = int.Parse(HttpContext.User.FindFirst("aud").Value);

            var response = new ResponseRevenue()
            {
                Revenues = revenueRepository.Revenues(),
                UsageRevenues = usageRepository.GetRevenueUsage(),
                Jars = jarRepository.GetAll(customerId)
            };
            return View("AllRevenue", response);
        }
        [Authorize]
        [HttpPost("add")]
        public IActionResult Add([FromForm] RequestRevenue requestRevenue)
        {
            if (string.IsNullOrEmpty(requestRevenue.NewRevenue))
            {
                Revenue revenue = new Revenue()
                {
                    UsageId = requestRevenue.UsageId,
                    Amount = requestRevenue.Amount,
                    TakenDate = requestRevenue.Date,
                    JarId = requestRevenue.JarId,
                    Note = requestRevenue.Note,
                };
                revenueRepository.Add(revenue);
                jarRepository.AddToJarNumber(requestRevenue.JarId, requestRevenue.Amount);
            }
            else
            {
                string newNameUsage = requestRevenue.NewRevenue;
                Usage usage = new Usage()
                {
                    Name = newNameUsage,
                    TypeId = 1,
                    Enable = true
                };
                usageRepository.AddUsage(usage);
                Revenue revenue = new Revenue()
                {
                    UsageId = usageRepository.GetByName(newNameUsage),
                    Amount = requestRevenue.Amount,
                    TakenDate = requestRevenue.Date,
                    JarId = requestRevenue.JarId,
                    Note = requestRevenue.Note,
                };
                revenueRepository.Add(revenue);
                jarRepository.AddToJarNumber(requestRevenue.JarId, requestRevenue.Amount);
            }
            return RedirectToAction("revenues");
        }
    }
}
