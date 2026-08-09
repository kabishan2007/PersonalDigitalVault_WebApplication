using Microsoft.AspNetCore.Mvc;

namespace PersonalDigitalVault_WebApplication.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
