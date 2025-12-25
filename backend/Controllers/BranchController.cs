using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    public class BranchController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
