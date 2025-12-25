using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    public class TicketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
