using Microsoft.AspNetCore.Mvc;
using REST_API.Data;

namespace REST_API.Controllers
{
    public class BudgetController : Controller
    {
        private readonly ApiDbContext _DbContext;
        public BudgetController(ApiDbContext DbContext)
        {
            _DbContext = DbContext;
        }
    }
}
