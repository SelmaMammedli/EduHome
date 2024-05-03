using EduHome.Areas.AdminArea.Views.ViewModels.Location;
using EduHome.Areas.AdminArea.Views.ViewModels.Notice;
using EduHome.DAL;
using EduHome.Models;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class LocationController : Controller
    {
        private readonly AppDbContext _context;
        public LocationController(AppDbContext context)
        {
            _context = context;
            
        }
        public IActionResult Index()
        {
            var datas=_context.Locations.ToList();
            return View(datas);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(LocationCreateVM createVM)
        {
            var location = new Location();
            location.Description = createVM.Description;
            location.Title = createVM.Title;
            location.Icon=createVM.Icon;
            _context.Locations.Add(location);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int? id)
        {
            if (id is null) return NotFound();
            var existLocation = _context.Locations.FirstOrDefault(s => s.Id == id);
            if (existLocation == null) return NotFound();
            LocationUpdateVM locationUpdateVM = new LocationUpdateVM();
            locationUpdateVM = new LocationUpdateVM { Title = existLocation.Title, Description = existLocation.Description ,Icon=existLocation.Icon};
            return View(locationUpdateVM);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Update(LocationUpdateVM updateVM, int? id)
        {
            if (id is null) return NotFound();
            var existLocation = _context.Locations.FirstOrDefault(s => s.Id == id);
            if (existLocation == null) return NotFound();
            existLocation.Description = updateVM.Description;
            existLocation.Title = updateVM.Title;
            existLocation.Icon = updateVM.Icon;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            if (id is null) return NotFound();
            var existLocation = _context.Locations.FirstOrDefault(s => s.Id == id);
            if (existLocation == null) return NotFound();
            return View(existLocation);
        }
        public IActionResult DeleteLocation(int? id)
        {
            if (id is null) return NotFound();
            var existLocation = _context.Locations.FirstOrDefault(s => s.Id == id);
            if (existLocation == null) return NotFound();

            _context.Locations.Remove(existLocation);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Detail(int? id)
        {
            if (id is null) return NotFound();
            var existLocation = _context.Locations.FirstOrDefault(s => s.Id == id);
            if (existLocation == null) return NotFound();
            return View(existLocation);
        }
    }
}
