using EduHome.Areas.AdminArea.Views.ViewModels.WhyChoose;
using EduHome.DAL;
using EduHome.Models;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class WhyChooseController : Controller
    {
        private readonly AppDbContext _context;

        public WhyChooseController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var datas=_context.WhyYouChooses.ToList();
            return View(datas);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(WhyChooseCreateVM createVM)
        {
            var why = new WhyYouChoose();
            why.Description = createVM.Description;
            why.Title = createVM.Title;
            _context.WhyYouChooses.Add(why);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int? id)
        {
            if (id is null) return NotFound();
            var existWhy = _context.WhyYouChooses.FirstOrDefault(s => s.Id == id);
            if (existWhy == null) return NotFound();
            WhyChooseUpdateVM whyUpdateVM = new WhyChooseUpdateVM();
            whyUpdateVM = new WhyChooseUpdateVM { Title = existWhy.Title, Description = existWhy.Description };
            return View(whyUpdateVM);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Update(WhyChooseUpdateVM updateVM, int? id)
        {
            if (id is null) return NotFound();
            var existWhy = _context.WhyYouChooses.FirstOrDefault(s => s.Id == id);
            if (existWhy == null) return NotFound();
            existWhy.Title = updateVM.Title;
            existWhy.Description = updateVM.Description;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            if (id is null) return NotFound();
            var existWhy = _context.WhyYouChooses.FirstOrDefault(s => s.Id == id);
            if (existWhy == null) return NotFound();
            return View(existWhy);
        }
        public IActionResult DeleteWhy(int? id)
        {
            if (id is null) return NotFound();
            var existWhy = _context.WhyYouChooses.FirstOrDefault(s => s.Id == id);
            if (existWhy == null) return NotFound();

            _context.WhyYouChooses.Remove(existWhy);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Detail(int? id)
        {
            if (id is null) return NotFound();
            var existWhy = _context.WhyYouChooses.FirstOrDefault(s => s.Id == id);
            if (existWhy == null) return NotFound();
            return View(existWhy);
        }
    }
}
