using Microsoft.AspNetCore.Mvc;

namespace PersonalDigitalVault_WebApplication.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
