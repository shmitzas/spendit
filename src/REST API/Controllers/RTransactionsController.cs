using Microsoft.AspNetCore.Mvc;
using REST_API.Data;

namespace REST_API.Controllers
{
    public class RTransactionsController : Controller
    {
        private readonly ApiDbContext _DbContext;
        public RTransactionsController(ApiDbContext DbContext)
        {
            _DbContext = DbContext;
        }
    }
}
