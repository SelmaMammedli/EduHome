using EduHome.DAL;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;
        public ContactController(AppDbContext context)
        {
            _context = context;
            
        }
        public IActionResult Index()
        {
            var datas=_context.Contacts.ToList();
            return View(datas);
        }
        public IActionResult Delete(int? id)
        {
            if (id is null) return NotFound();
            var existContact = _context.Contacts.FirstOrDefault(s => s.Id == id);
            if (existContact == null) return NotFound();
            return View(existContact);
        }
        public IActionResult DeleteContact(int? id)
        {
            if (id is null) return NotFound();
            var existContact = _context.Contacts.FirstOrDefault(s => s.Id == id);
            if (existContact == null) return NotFound();


            _context.Contacts.Remove(existContact);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Detail(int?id)
        {
            if (id is null) return NotFound();
            var existContact = _context.Contacts.FirstOrDefault(s => s.Id == id);
            if (existContact == null) return NotFound();
            return View(existContact);
        }
    }
}
