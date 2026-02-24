using Microsoft.AspNetCore.Mvc;

namespace UserManagement.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
