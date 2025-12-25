using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    public class DepartmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
