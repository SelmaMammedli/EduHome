using EduHome.Areas.AdminArea.Views.ViewModels.Language;
using EduHome.Areas.AdminArea.Views.ViewModels.Tag;
using EduHome.DAL;
using EduHome.Models;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class TagController : Controller
    {
        private readonly AppDbContext _context;
        public TagController(AppDbContext context)
        {
            _context = context;
            
        }
        public IActionResult Index()
        {
            var datas=_context.Tags.ToList();
            return View(datas);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(TagCreateVM createVM)
        {
            var tag = new Tag();
            tag.Title = createVM.Title;

            _context.Tags.Add(tag);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int? id)
        {
            if (id is null) return NotFound();
            var existTag = _context.Tags.FirstOrDefault(s => s.Id == id);
            if (existTag == null) return NotFound();
            TagUpdateVM tagVM = new TagUpdateVM();
            tagVM = new TagUpdateVM { Title = existTag.Title };
            return View(tagVM);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Update(TagUpdateVM updateVM, int? id)
        {
            if (id is null) return NotFound();
            var existTag = _context.Tags.FirstOrDefault(s => s.Id == id);
            if (existTag == null) return NotFound();
            existTag.Title = updateVM.Title;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            if (id is null) return NotFound();
            var existTag = _context.Tags.FirstOrDefault(s => s.Id == id);
            if (existTag == null) return NotFound();
            return View(existTag);
        }
        public IActionResult DeleteTags(int? id)
        {
            if (id is null) return NotFound();
            var existTag = _context.Tags.FirstOrDefault(s => s.Id == id);
            if (existTag == null) return NotFound();

            _context.Tags.Remove(existTag);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Detail(int? id)
        {
            if (id is null) return NotFound();
            var existTag = _context.Tags.FirstOrDefault(s => s.Id == id);
            if (existTag == null) return NotFound();
            return View(existTag);
        }
    }
}
