using Microsoft.AspNetCore.Mvc;

namespace EduHome.Areas.AdminArea.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
