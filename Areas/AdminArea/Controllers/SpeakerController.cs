using EduHome.Areas.AdminArea.Views.ViewModels.Slider;
using EduHome.Areas.AdminArea.Views.ViewModels.Speaker;
using EduHome.DAL;
using EduHome.Extensions;
using EduHome.Helper;
using EduHome.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class SpeakerController : Controller
    {
        private readonly AppDbContext _context;
        public SpeakerController(AppDbContext context)
        {
            _context = context;
            
        }
        public IActionResult Index()
        {
            var datas=_context.Speakers.AsNoTracking().ToList();
            return View(datas);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(SpeakerCreateVM createVM)
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
            var speaker = new Speaker() { ImageUrl = photos.SaveFile("img/teacher"), FullName = createVM.FullName, Profession = createVM.Profession,Degree=createVM.Degree };
            _context.Speakers.Add(speaker);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int? id)
        {
            if (id is null) return NotFound();
            var existSpeaker = _context.Speakers.FirstOrDefault(s => s.Id == id);
            if (existSpeaker == null) return NotFound();
            SpeakerUpdateVM speakerUpdateVM = new SpeakerUpdateVM();
            speakerUpdateVM = new SpeakerUpdateVM { ImageUrl = existSpeaker.ImageUrl, FullName = existSpeaker.FullName, Degree = existSpeaker.Degree,Profession=existSpeaker.Profession };
            return View(speakerUpdateVM);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Update(int? id, SpeakerUpdateVM updateVM)
        {
            if (id is null) return NotFound();
            var existSpeaker = _context.Speakers.FirstOrDefault(s => s.Id == id);
            if (existSpeaker == null) return NotFound();
            var photo = updateVM.Photo;
            updateVM.ImageUrl = existSpeaker.ImageUrl;

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


                string fileName = photo.SaveFile("img/teacher");
                DeleteFileHelper.DeleteFile("img/teacher", existSpeaker.ImageUrl);
                existSpeaker.ImageUrl = fileName;

            };
            existSpeaker.FullName=updateVM.FullName;
            existSpeaker.Degree = updateVM.Degree;
            existSpeaker.Profession = updateVM.Profession;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            if (id is null) return NotFound();
            var existSpeaker = _context.Speakers.FirstOrDefault(s => s.Id == id);
            if (existSpeaker == null) return NotFound();
            return View(existSpeaker);
        }
        public IActionResult DeleteSpeaker(int? id)
        {
            if (id is null) return NotFound();
            var existSpeaker = _context.Speakers.FirstOrDefault(s => s.Id == id);
            if (existSpeaker == null) return NotFound();
            DeleteFileHelper.DeleteFile("img/teacher", existSpeaker.ImageUrl);

            _context.Speakers.Remove(existSpeaker);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Detail(int? id)
        {
            if (id is null) return NotFound();
            var existSpeaker = _context.Speakers.FirstOrDefault(s => s.Id == id);
            if (existSpeaker == null) return NotFound();
            return View(existSpeaker);
        }
    }
}
