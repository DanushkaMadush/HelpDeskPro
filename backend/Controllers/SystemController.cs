using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    public class SystemController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
