
using EduHome.DAL;
using EduHome.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EduHome.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeVM homeVm = new HomeVM();
            homeVm.Sliders = _context.Sliders.ToList();
            return View(homeVm);
        }
        public IActionResult Courses()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Blog()
        {
            return View();
        }
        public IActionResult Event()
        {
            return View();
        }
        public IActionResult Teacher()
        {
            return View();
        }


    }
}