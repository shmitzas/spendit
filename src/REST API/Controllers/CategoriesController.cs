using Microsoft.AspNetCore.Mvc;

namespace REST_API.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
