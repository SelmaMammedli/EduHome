using Microsoft.AspNetCore.Mvc;

namespace EduHome.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class CoursesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
