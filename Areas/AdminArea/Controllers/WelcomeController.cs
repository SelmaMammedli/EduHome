using EduHome.Areas.AdminArea.Views.ViewModels.Welcome;
using EduHome.Areas.AdminArea.Views.ViewModels.WhyChoose;
using EduHome.DAL;
using EduHome.Extensions;
using EduHome.Helper;
using EduHome.Models;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class WelcomeController : Controller
    {
        private readonly AppDbContext _context;
        public WelcomeController(AppDbContext context)
        {
            _context = context;
            
        }
        public IActionResult Index()
        {
            var datas=_context.Welcomes.ToList();
            return View(datas);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(WelcomeCreateVM createVM)
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
            var welcome = new Welcome() { ImageUrl = photos.SaveFile("img/about"), Description = createVM.Description, VideoLink = createVM.VideoLink, Title = createVM.Title };
            _context.Welcomes.Add(welcome);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int? id)
        {
            if (id is null) return NotFound();
            var existWelcome = _context.Welcomes.FirstOrDefault(s => s.Id == id);
            if (existWelcome == null) return NotFound();
            WelcomeUpdateVM welcomeVM = new WelcomeUpdateVM();
            welcomeVM = new WelcomeUpdateVM { VideoLink=existWelcome.VideoLink,ImageUrl=existWelcome.ImageUrl, Title = existWelcome.Title, Description = existWelcome.Description };
            return View(welcomeVM);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Update(WelcomeUpdateVM updateVM, int? id)
        {
            if (id is null) return NotFound();
            var existWelcome = _context.Welcomes.FirstOrDefault(s => s.Id == id);
            if (existWelcome == null) return NotFound();
            var photo = updateVM.Photo;
            updateVM.ImageUrl = existWelcome.ImageUrl;

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


                string fileName = photo.SaveFile("img/about");
                DeleteFileHelper.DeleteFile("img/about", existWelcome.ImageUrl);
                existWelcome.ImageUrl = fileName;

            };
            existWelcome.Title = updateVM.Title;
            existWelcome.Description = updateVM.Description;
            existWelcome.VideoLink = updateVM.VideoLink;
            
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            if (id is null) return NotFound();
            var existWelcome = _context.Welcomes.FirstOrDefault(s => s.Id == id);
            if (existWelcome == null) return NotFound();
            return View(existWelcome);
        }
        public IActionResult DeleteWelcome(int? id)
        {
            if (id is null) return NotFound();
            var existWelcome = _context.Welcomes.FirstOrDefault(s => s.Id == id);
            if (existWelcome == null) return NotFound();
            DeleteFileHelper.DeleteFile("img/about", existWelcome.ImageUrl);
            _context.Welcomes.Remove(existWelcome);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Detail(int? id)
        {
            if (id is null) return NotFound();
            var existWelcome = _context.Welcomes.FirstOrDefault(s => s.Id == id);
            if (existWelcome == null) return NotFound();
            return View(existWelcome);
        }
    }
}
