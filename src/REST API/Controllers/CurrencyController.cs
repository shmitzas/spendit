using Microsoft.AspNetCore.Mvc;
using REST_API.Data;

namespace REST_API.Controllers
{
    public class CurrencyController : Controller
    {
        private readonly ApiDbContext _DbContext;
        public CurrencyController(ApiDbContext DbContext)
        {
            _DbContext = DbContext;
        }
    }
}
