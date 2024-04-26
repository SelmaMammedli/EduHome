using EduHome.Areas.AdminArea.Views.ViewModels.Welcome;
using EduHome.Areas.AdminArea.Views.ViewModels.WhyChoose;
using EduHome.DAL;
using EduHome.Models;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class WelcomeController : Controller
    {
        private readonly AppDbContext _context;
        public WelcomeController(AppDbContext context)
        {
            _context = context;
            
        }
        public IActionResult Index()
        {
            var datas=_context.Welcomes.ToList();
            return View(datas);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(WelcomeCreateVM createVM)
        {
            var welcome = new Welcome();
            welcome.Title=createVM.Title;
            welcome.Description=createVM.Description;
            _context.Welcomes.Add(welcome);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int? id)
        {
            if (id is null) return NotFound();
            var existWelcome = _context.Welcomes.FirstOrDefault(s => s.Id == id);
            if (existWelcome == null) return NotFound();
            WelcomeUpdateVM welcomeVM = new WelcomeUpdateVM();
            welcomeVM = new WelcomeUpdateVM { Title = existWelcome.Title, Description = existWelcome.Description };
            return View(welcomeVM);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Update(WelcomeUpdateVM updateVM, int? id)
        {
            if (id is null) return NotFound();
            var existWelcome = _context.Welcomes.FirstOrDefault(s => s.Id == id);
            if (existWelcome == null) return NotFound();
            existWelcome.Title = updateVM.Title;
            existWelcome.Description = updateVM.Description;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            if (id is null) return NotFound();
            var existWelcome = _context.Welcomes.FirstOrDefault(s => s.Id == id);
            if (existWelcome == null) return NotFound();
            return View(existWelcome);
        }
        public IActionResult DeleteWelcome(int? id)
        {
            if (id is null) return NotFound();
            var existWelcome = _context.Welcomes.FirstOrDefault(s => s.Id == id);
            if (existWelcome == null) return NotFound();

            _context.Welcomes.Remove(existWelcome);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Detail(int? id)
        {
            if (id is null) return NotFound();
            var existWelcome = _context.Welcomes.FirstOrDefault(s => s.Id == id);
            if (existWelcome == null) return NotFound();
            return View(existWelcome);
        }
    }
}
