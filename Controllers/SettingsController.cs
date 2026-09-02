using Microsoft.AspNetCore.Mvc;

namespace BMS.Controllers
{
    public class SettingsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
