using Microsoft.AspNetCore.Mvc;

namespace REST_API.Controllers
{
    public class RTransactionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
