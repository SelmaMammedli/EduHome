using EduHome.Areas.AdminArea.Views.ViewModels.License;
using EduHome.Areas.AdminArea.Views.ViewModels.Welcome;
using EduHome.DAL;
using EduHome.Models;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class LicenseController : Controller
    {
        private readonly AppDbContext _context;
        public LicenseController(AppDbContext context)
        {
            _context = context;
            
        }
        public IActionResult Index()
        {
            var datas=_context.Licenses.ToList();
            return View(datas);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(LicenseCreateVM createVM)
        {
            var license = new License();
            license.Title = createVM.Title;
            
            _context.Licenses.Add(license);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int? id)
        {
            if (id is null) return NotFound();
            var existLicense = _context.Licenses.FirstOrDefault(s => s.Id == id);
            if (existLicense == null) return NotFound();
            LicenseUpdateVM licenseVM = new LicenseUpdateVM();
            licenseVM = new LicenseUpdateVM { Title = existLicense.Title };
            return View(licenseVM);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Update(LicenseUpdateVM updateVM, int? id)
        {
            if (id is null) return NotFound();
            var existLicense = _context.Licenses.FirstOrDefault(s => s.Id == id);
            if (existLicense == null) return NotFound();
            existLicense.Title = updateVM.Title;
            
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            if (id is null) return NotFound();
            var existLicense = _context.Licenses.FirstOrDefault(s => s.Id == id);
            if (existLicense == null) return NotFound();
            return View(existLicense);
        }
        public IActionResult DeleteLicense(int? id)
        {
            if (id is null) return NotFound();
            var existLicense = _context.Licenses.FirstOrDefault(s => s.Id == id);
            if (existLicense == null) return NotFound();

            _context.Licenses.Remove(existLicense);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Detail(int? id)
        {
            if (id is null) return NotFound();
            var existLicense = _context.Licenses.FirstOrDefault(s => s.Id == id);
            if (existLicense == null) return NotFound();
            return View(existLicense);
        }
    }
}
