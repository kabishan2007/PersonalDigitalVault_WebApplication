using Microsoft.AspNetCore.Mvc;

namespace PersonalDigitalVault_WebApplication.Controllers
{
    public class DocumentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
