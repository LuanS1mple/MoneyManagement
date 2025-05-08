using Microsoft.AspNetCore.Mvc;
using MM.Usecase;

namespace MM.ResourceProvider.Controllers.Jar
{
    public class JarController : Controller
    {
        private readonly IJarRepository jarRepository;
        public JarController(IJarRepository jarRepository)
        {
            this.jarRepository = jarRepository;
        }
        public IActionResult Jars(int customerId)
        {
            string jarData = ;
        }
        public IActionResult AddJar()
        {
            return View();
        }
        public IActionResult UpdateJar()
        {
            return View();
        }
        public IActionResult DeleteJar()
        {
            return View();
        }
    }
}
