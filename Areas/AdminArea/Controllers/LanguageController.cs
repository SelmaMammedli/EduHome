using EduHome.Areas.AdminArea.Views.ViewModels.Language;
using EduHome.Areas.AdminArea.Views.ViewModels.License;
using EduHome.DAL;
using EduHome.Models;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class LanguageController : Controller
    {
        private readonly AppDbContext _context;
        public LanguageController(AppDbContext context)
        {
            _context = context;
            
        }
        public IActionResult Index()
        {
            var datas=_context.Languages.ToList();
            return View(datas);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(LanguageCreateVM createVM)
        {
            var license = new Language();
            license.Title = createVM.Title;

            _context.Languages.Add(license);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int? id)
        {
            if (id is null) return NotFound();
            var existLicense = _context.Languages.FirstOrDefault(s => s.Id == id);
            if (existLicense == null) return NotFound();
            LanguageUpdateVM licenseVM = new LanguageUpdateVM();
            licenseVM = new LanguageUpdateVM { Title = existLicense.Title };
            return View(licenseVM);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Update(LanguageUpdateVM updateVM, int? id)
        {
            if (id is null) return NotFound();
            var existLicense = _context.Languages.FirstOrDefault(s => s.Id == id);
            if (existLicense == null) return NotFound();
            existLicense.Title = updateVM.Title;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            if (id is null) return NotFound();
            var existLicense = _context.Languages.FirstOrDefault(s => s.Id == id);
            if (existLicense == null) return NotFound();
            return View(existLicense);
        }
        public IActionResult DeleteLanguage(int? id)
        {
            if (id is null) return NotFound();
            var existLicense = _context.Languages.FirstOrDefault(s => s.Id == id);
            if (existLicense == null) return NotFound();

            _context.Languages.Remove(existLicense);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Detail(int? id)
        {
            if (id is null) return NotFound();
            var existLicense = _context.Languages.FirstOrDefault(s => s.Id == id);
            if (existLicense == null) return NotFound();
            return View(existLicense);
        }
    }
}
