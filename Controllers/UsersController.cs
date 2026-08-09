using Microsoft.AspNetCore.Mvc;

namespace PersonalDigitalVault_WebApplication.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
