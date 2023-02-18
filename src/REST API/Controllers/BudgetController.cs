using Microsoft.AspNetCore.Mvc;

namespace REST_API.Controllers
{
    public class BudgetController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
