
using EduHome.Areas.AdminArea.Views.ViewModels.Category;
using EduHome.DAL;
using EduHome.Models;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            _context = context;
            
        }
        public IActionResult Index()
        {
            var category=_context.Categories.ToList();
            return View(category);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(CategoryCreateVM createVM)
        {
            var category = new Category();
            category.Description = createVM.Description;
            category.Title = createVM.Title;
            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int? id)
        {
            if (id is null) return NotFound();
            var existCategory = _context.Categories.FirstOrDefault(s => s.Id == id);
            if (existCategory == null) return NotFound();
            CategoryUpdateVM categoryUpdateVM = new CategoryUpdateVM();
            categoryUpdateVM = new CategoryUpdateVM { Title = existCategory.Title, Description = existCategory.Description };
            return View(categoryUpdateVM);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Update(CategoryUpdateVM updateVM, int? id)
        {
            if (id is null) return NotFound();
            var existCategory = _context.Categories.FirstOrDefault(s => s.Id == id);
            if (existCategory == null) return NotFound();
            existCategory.Title = updateVM.Title;
            existCategory.Description = updateVM.Description;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            if (id is null) return NotFound();
            var existCategory = _context.Categories.FirstOrDefault(s => s.Id == id);
            if (existCategory == null) return NotFound();
            return View(existCategory);
        }
        public IActionResult DeleteCategory(int? id)
        {
            if (id is null) return NotFound();
            var existCategory = _context.Categories.FirstOrDefault(s => s.Id == id);
            if (existCategory == null) return NotFound();

            _context.Categories.Remove(existCategory);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Detail(int? id)
        {
            if (id is null) return NotFound();
            var existCategory = _context.Categories.FirstOrDefault(s => s.Id == id);
            if (existCategory == null) return NotFound();
            return View(existCategory);
        }
    }
}
