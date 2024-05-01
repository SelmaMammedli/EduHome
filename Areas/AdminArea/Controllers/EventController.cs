using EduHome.Areas.AdminArea.Views.ViewModels.Course;
using EduHome.Areas.AdminArea.Views.ViewModels.Event;
using EduHome.DAL;
using EduHome.Extensions;
using EduHome.Helper;
using EduHome.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EduHome.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class EventController : Controller
    {
        private readonly AppDbContext _context;
        public EventController(AppDbContext context)
        {
            _context = context;
            
        }
        public IActionResult Index()
        {
            var events=_context.Events
                .Include(e=>e.EventSpeakers)
                .ThenInclude(es=>es.Speaker)
                .AsNoTracking()
                .ToList();
            return View(events);
        }
        public IActionResult Create()
        {
           // ViewBag.Speakers=_context.Speakers.ToList();
           ViewBag.Speakers=new SelectList(_context.Speakers.ToList(),"Id","FullName");
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(EventCreateVM createVM)
        {
            ViewBag.Speakers = new SelectList(_context.Speakers.ToList(), "Id", "FullName");
            if(!ModelState.IsValid)
            {
                return View(createVM);
            }
            Event newEvent = new Event();
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
            List<EventSpeaker> list = new();
            foreach (var speakerId in createVM.SpeakerIds)
            {
                EventSpeaker eventSpeaker = new();
                eventSpeaker.EventId = newEvent.Id;
                eventSpeaker.SpeakerId=speakerId;
                list.Add(eventSpeaker);
            }
           
            newEvent.Title = createVM.Title;
            newEvent.Description = createVM.Description;
            newEvent.Venue = createVM.Venue;
            newEvent.StartDate= createVM.StartDate;
            newEvent.EndDate= createVM.EndDate;
            newEvent.ImageUrl = photos.SaveFile("img/event");
            newEvent.EventSpeakers = list;
            //newEvent.EventSpeakers = new();
            _context.Events.Add(newEvent);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int? id)
        {
            ViewBag.Speakers = new SelectList(_context.Speakers.ToList(), "Id", "FullName");
            if (id == null) return NotFound();
            var existEvent = _context.Events
                .Include(c => c.EventSpeakers)
                .ThenInclude(c => c.Speaker)
                .FirstOrDefault(c => c.Id == id);
            if (existEvent == null) return NotFound();
            EventUpdateVM eventUpdateVM = new EventUpdateVM();
            eventUpdateVM.StartDate = existEvent.StartDate;
            eventUpdateVM.EndDate = existEvent.EndDate;
            eventUpdateVM.Venue= existEvent.Venue;
            eventUpdateVM.Title = existEvent.Title;
            eventUpdateVM.Description=existEvent.Description;
            eventUpdateVM.ImageUrl= existEvent.ImageUrl;
           // eventUpdateVM.SpeakerIds = existEvent.SpeakerIds;
            foreach (var speakerId in existEvent.SpeakerIds)
            {
                EventSpeaker eventSpeaker = new();
                existEvent.Id = eventSpeaker.EventId;
                eventSpeaker.SpeakerId = speakerId;
                
            }
            return View(eventUpdateVM);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Update(int?id,EventUpdateVM eventUpdateVM)
        {
            ViewBag.Speakers = new SelectList(_context.Speakers.ToList(), "Id", "FullName");
            if (id == null) return NotFound();
            var existEvent = _context.Events
                .Include(c => c.EventSpeakers)
                .ThenInclude(c => c.Speaker)
                .FirstOrDefault(c => c.Id == id);
            if (existEvent == null) return NotFound();
            var photos = eventUpdateVM.Photo;
            eventUpdateVM.ImageUrl = existEvent.ImageUrl;
            if (photos != null && photos.Length > 0)
            {
                    if (!photos.CheckFile())
                    {
                        ModelState.AddModelError("Photo", "Please upload right file");
                        return View(eventUpdateVM);
                    }
                    if (photos.CheckSize(1000))
                    {
                        ModelState.AddModelError("Photo", "Please choose normal file");
                        return View(eventUpdateVM);
                    }
                    string fileName = photos.SaveFile("img/event");
                    DeleteFileHelper.DeleteFile("img/event", existEvent.ImageUrl);
                    existEvent.ImageUrl = fileName;
                
            };
            List<EventSpeaker> list = new();
            foreach (var speakerId in existEvent.SpeakerIds)
            {
                EventSpeaker eventSpeaker = new();
                 existEvent.Id=eventSpeaker.EventId;
                eventSpeaker.SpeakerId = speakerId;
                list.Add(eventSpeaker);
            }

            existEvent.StartDate=eventUpdateVM.StartDate;
            existEvent.EndDate=eventUpdateVM.EndDate;
            existEvent.Title=eventUpdateVM.Title;
            existEvent.Description=eventUpdateVM.Description;
            existEvent.Venue=eventUpdateVM.Venue;
            list = eventUpdateVM.EventSpeakers;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            if (id is null) return NotFound();
            var existEvent = _context.Events
                .Include(c => c.EventSpeakers)
                .ThenInclude(c => c.Speaker)
                .FirstOrDefault(c => c.Id == id);
            if (existEvent == null) return NotFound();
            return View(existEvent);
        }
        public IActionResult DeleteEvent(int? id)
        {
            if (id is null) return NotFound();
            var existEvent = _context.Events
                .Include(c => c.EventSpeakers)
                .ThenInclude(c => c.Speaker)
                .FirstOrDefault(c => c.Id == id);
            if (existEvent == null) return NotFound();
            DeleteFileHelper.DeleteFile("img/event", existEvent.ImageUrl);
            _context.Events.Remove(existEvent);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Detail(int? id)
        {
            if (id is null) return NotFound();
            var existEvent = _context.Events
                .Include(c => c.EventSpeakers)
                .ThenInclude(c => c.Speaker)
                .FirstOrDefault(c => c.Id == id);
            if (existEvent == null) return NotFound();
            return View(existEvent);
        }
    }
}
