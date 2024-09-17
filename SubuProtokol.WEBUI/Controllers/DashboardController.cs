using Microsoft.AspNetCore.Mvc;

namespace SubuProtokol.WEBUI.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
