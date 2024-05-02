using EduHome.Areas.AdminArea.Views.ViewModels.Bios;
using EduHome.Areas.AdminArea.Views.ViewModels.Board;
using EduHome.DAL;
using EduHome.Models;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class BiosController : Controller
    {
        private readonly AppDbContext _context;
        public BiosController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var datas=_context.Bios.ToList();
            return View(datas);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(BiosCreateVM createVM)
        {
            var bios = new Bio();
            bios.Key = createVM.Key;
            bios.Value = createVM.Value;
            _context.Bios.Add(bios);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int? id)
        {
            if (id is null) return NotFound();
            var existBios = _context.Bios.FirstOrDefault(s => s.Id == id);
            if (existBios == null) return NotFound();
            BiosUpdateVM biosUpdateVM = new BiosUpdateVM();
            biosUpdateVM = new BiosUpdateVM { Key = existBios.Key, Value = existBios.Value };
            return View(biosUpdateVM);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Update(BiosUpdateVM updateVM, int? id)
        {
            if (id is null) return NotFound();
            var existBios = _context.Bios.FirstOrDefault(s => s.Id == id);
            if (existBios == null) return NotFound();
            existBios.Key = updateVM.Key;
            existBios.Value = updateVM.Value;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            if (id is null) return NotFound();
            var existBios = _context.Bios.FirstOrDefault(s => s.Id == id);
            if (existBios == null) return NotFound();
            return View(existBios);
        }
        public IActionResult DeleteBios(int? id)
        {
            if (id is null) return NotFound();
            var existBios = _context.Bios.FirstOrDefault(s => s.Id == id);
            if (existBios == null) return NotFound();

            _context.Bios.Remove(existBios);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Detail(int? id)
        {
            if (id is null) return NotFound();
            var existBios = _context.Bios.FirstOrDefault(s => s.Id == id);
            if (existBios == null) return NotFound();
            return View(existBios);
        }
    }
}
