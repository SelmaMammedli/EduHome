
using EduHome.DAL;
using EduHome.Models;
using EduHome.ViewModels;
using EduHome.ViewModels.Contact;
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
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contact(ContactVM contactVM)
        {
            Contact contact = new();
            contact.Name = contactVM.Name;
            contact.Email = contactVM.Email;
            contact.Subject = contactVM.Subject;
            contact.Message = contactVM.Message;
            _context.Add(contact);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}