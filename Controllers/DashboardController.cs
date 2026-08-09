using Microsoft.AspNetCore.Mvc;

namespace PersonalDigitalVault_WebApplication.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
