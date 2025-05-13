using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MM.HostApp.Models;
using MM.Usecase;

namespace MM.HostApp.Controllers.ExpenditureManagement
{
    [Route("expenditure")]
    public class ExpenditureController : Controller
    {
        private readonly IExpenditureRepository expenditureRepository;
        private readonly IUsageRepository usageRepository;
        private readonly IJarRepository jarRepository;
        public ExpenditureController(IExpenditureRepository expenditureRepository, IUsageRepository usageRepository, IJarRepository jar)
        {
            this.usageRepository = usageRepository;
            this.expenditureRepository = expenditureRepository;
            this.jarRepository = jar;
        }
        [Authorize]
        [HttpGet("expens")]
        public IActionResult Index()
        {
            var customerId = int.Parse(HttpContext.User.FindFirst("aud").Value);

            var respones = new ResponseExpenditure()
            {
                ExpenditureUsage = usageRepository.GetExpenditureUsage(),
                Expenditures = expenditureRepository.Expenditures(),
                ExpenditureJar = jarRepository.GetAll(customerId)
            };
            return View("AllExpenditure", respones);
        }
        [Authorize]
        [HttpPost("add")]
        public IActionResult Add([FromForm] RequestExpenditure request)
        {
            Jar jar = jarRepository.GetById(request.JarId);
            if (request.Amount > jar.Total)
            {
                var customerId = int.Parse(HttpContext.User.FindFirst("aud").Value);

                var respones = new ResponseExpenditure()
                {
                    Message = "Không đủ số dư",
                    ExpenditureUsage = usageRepository.GetExpenditureUsage(),
                    Expenditures = expenditureRepository.Expenditures(),
                    ExpenditureJar = jarRepository.GetAll(customerId)
                };
                return View("AllExpenditure", respones);
            }
            if (string.IsNullOrEmpty(request.NewExpenditure))
            {
                var expen = new Expenditure()
                {
                    UsageId = request.UsageId,
                    Amount = request.Amount,
                    UsedDate = request.Date,
                    JarId = request.JarId,
                    Note = request.Note,
                };
                jarRepository.AddToJarNumber(request.JarId, -1 * request.Amount);
                expenditureRepository.Add(expen);
            }
            else
            {
                var usage = new Usage()
                {
                    Name = request.NewExpenditure,
                    TypeId = 2,
                    Enable = true
                };
                usageRepository.AddUsage(usage);
                Expenditure expen = new Expenditure()
                {
                    UsageId = usageRepository.GetByName(request.NewExpenditure),
                    Amount = request.Amount,
                    UsedDate = request.Date,
                    JarId = request.JarId,
                    Note = request.Note,
                };
                expenditureRepository.Add(expen);
                jarRepository.AddToJarNumber(request.JarId, -1 * request.Amount);
            }
            return RedirectToAction("expens");
        }
    }
}
