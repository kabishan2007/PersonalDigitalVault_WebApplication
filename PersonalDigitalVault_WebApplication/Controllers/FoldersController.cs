using Microsoft.AspNetCore.Mvc;

namespace PersonalDigitalVault_WebApplication.Controllers
{
    public class FoldersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
