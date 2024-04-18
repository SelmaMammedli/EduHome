using EduHome.Areas.AdminArea.Views.ViewModels.Slider;
using EduHome.DAL;
using EduHome.Extensions;
using EduHome.Helper;
using EduHome.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;

        public SliderController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var datas=_context.Sliders.AsNoTracking().ToList();
            return View(datas);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(SliderCreateVM createVM)
        {

            var photos = createVM.Photo;
            if (photos == null || photos.Length == 0)
            {
                ModelState.AddModelError("Photo", "Please upload file");
                return View();
            }
            if (!photos.CheckFile())
            {
                ModelState.AddModelError("Photo", "Please upload right file");
                return View();
            }
            if (photos.CheckSize(5000))
            {
                ModelState.AddModelError("Photo", "Please choose normal file");
                return View();
            }
            var slider = new Slider() { ImageUrl = photos.SaveFile("img/slider"),Title=createVM.Title,Descrription=createVM.Descrription};
            _context.Sliders.Add(slider);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int? id)
        {
            if (id is null) return NotFound();
            var existSlider = _context.Sliders.FirstOrDefault(s => s.Id == id);
            if (existSlider == null) return NotFound();
            SliderUpdateVM sliderUpdateVM= new SliderUpdateVM();
            sliderUpdateVM = new SliderUpdateVM { ImageUrl = existSlider.ImageUrl,Title=existSlider.Title,Descrription=existSlider.Descrription};
            return View(sliderUpdateVM);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Update(int? id, SliderUpdateVM updateVM)
        {
            if (id is null) return NotFound();
            var existSlider = _context.Sliders.FirstOrDefault(s => s.Id == id);
            if (existSlider == null) return NotFound();
            var photo = updateVM.Photo;
            updateVM.ImageUrl = existSlider.ImageUrl;

            if (photo is not null && photo.Length > 0) 
            {
                if (!photo.CheckFile())
                {
                    ModelState.AddModelError("Photo", "Please upload right file");
                    return View(updateVM);
                }
                if (photo.CheckSize(5000))
                {
                    ModelState.AddModelError("Photo", "Please upload right file");
                    return View(updateVM);
                }


                string fileName = photo.SaveFile("img/slider");
                DeleteFileHelper.DeleteFile("img/slider", existSlider.ImageUrl);
                existSlider.ImageUrl = fileName;

            };
            
            existSlider.Descrription=updateVM.Descrription;
            existSlider.Title=updateVM.Title;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            if (id is null) return NotFound();
            var existSlider = _context.Sliders.FirstOrDefault(s => s.Id == id);
            if (existSlider == null) return NotFound();
            return View(existSlider);
        }
        public IActionResult DeleteSlider(int? id)
        {
            if (id is null) return NotFound();
            var existSlider = _context.Sliders.FirstOrDefault(s => s.Id == id);
            if (existSlider == null) return NotFound();
            DeleteFileHelper.DeleteFile("img/slider", existSlider.ImageUrl);

            _context.Sliders.Remove(existSlider);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Detail(int? id)
        {
            if (id is null) return NotFound();
            var existSlider = _context.Sliders.FirstOrDefault(s => s.Id == id);
            if (existSlider == null) return NotFound();
            return View(existSlider);
        }
    }
}
