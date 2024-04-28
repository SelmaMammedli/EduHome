using EduHome.DAL;
using EduHome.Models;
using EduHome.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.Controllers
{
    public class SubscribeController : Controller
    {
        private readonly AppDbContext _context;
        public SubscribeController(AppDbContext context)
        {
            _context = context;
            
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(SubscribeVM subscribeVM)
        {
            if (!ModelState.IsValid) return View();
            if (_context.Subscriptions.Any(s => s.Email.ToLower() == subscribeVM.Email.ToLower()))
            {
                ModelState.AddModelError("Email", "This email is existed...");
                return View();
            }
            Subscribe subscribe = new Subscribe();
            subscribe.Email = subscribeVM.Email;
            _context.Subscriptions.Add(subscribe);
            _context.SaveChanges();
            return RedirectToAction("Contact","Home");
        }
    }
}
