using EduHome.DAL;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class SubscriptionsController : Controller
    {
        private readonly AppDbContext _context;
        public SubscriptionsController(AppDbContext context)
        {
           _context = context;
            
        }
        public IActionResult Index()
        {
            var datas=_context.Subscriptions.ToList();
            return View(datas);
        }
        public IActionResult Delete(int? id)
        {
            if (id is null) return NotFound();
            var existSub = _context.Subscriptions.FirstOrDefault(s => s.Id == id);
            if (existSub == null) return NotFound();
            return View(existSub);
        }
        public IActionResult DeleteSubscriptions(int? id)
        {
            if (id is null) return NotFound();
            var existSub = _context.Subscriptions.FirstOrDefault(s => s.Id == id);
            if (existSub == null) return NotFound();
            _context.Subscriptions.Remove(existSub);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Detail(int? id)
        {
            if (id is null) return NotFound();
            var existSub = _context.Subscriptions.FirstOrDefault(s => s.Id == id);
            if (existSub == null) return NotFound();
            return View(existSub);
        }
    }
}
