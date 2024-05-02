
using EduHome.DAL;
using EduHome.Models;
using EduHome.ViewModels;
using EduHome.ViewModels.Contact;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EduHome.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeVM homeVm = new HomeVM();
            homeVm.Sliders = _context.Sliders.ToList();
            homeVm.NoticeBoards=_context.NoticesBoards.ToList();
            homeVm.Boards = _context.Boards.ToList();
            homeVm.Courses= _context.Courses.Include(c => c.Category).Include(c => c.Language).Take(3).ToList();
            homeVm.Blogs= _context.Blogs.Take(3).ToList();
            homeVm.Students=_context.Students.Include(c => c.Category).Take(3).ToList() ;
            homeVm.WhyYouChoose = _context.WhyYouChooses.FirstOrDefault();
            return View(homeVm);
        }
        public IActionResult Courses()
        {
            HomeVM homeVM = new HomeVM();
            homeVM.Courses= _context.Courses
                .Include(c=>c.Category)
                .Include(c => c.Language)
                .ToList();
            return View(homeVM);
        }
        public IActionResult CoursesDetails(int? id)
        {
            CoursesVM coursesVM = new CoursesVM();
            coursesVM.Blog = _context.Blogs.FirstOrDefault(c => c.Id == id);
            coursesVM.Blogs=_context.Blogs.Take(3).ToList();
            coursesVM.Categories= _context.Categories.ToList();
            coursesVM.Tags= _context.Tags.ToList();
            coursesVM.Courses = _context.Courses
                .Include(c => c.Category)
                .Include(c => c.Language)
                .FirstOrDefault(c => c.Id == id);
            return View(coursesVM);
        }
        public IActionResult About()
        {
            HomeVM homeVm = new HomeVM();
            homeVm.NoticeBoards = _context.NoticesBoards.ToList();
            homeVm.Welcome = _context.Welcomes.FirstOrDefault();
            homeVm.Teachers = _context.Teachers
                .
                Include(c => c.License)
                .Take(4).ToList();
            homeVm.Students = _context.Students.Include(c=>c.Category).Take(3).ToList();
            return View(homeVm);
        }
        public IActionResult Blog()
        {
            HomeVM homeVm = new HomeVM();
            homeVm.Blogs = _context.Blogs.ToList();
            return View(homeVm);
        }
        public IActionResult BlogDetails(int?id)
        {
            BlogVM blogVM= new BlogVM();
            blogVM.Blogs = _context.Blogs.Take(3).ToList();
            blogVM.Blog = _context.Blogs.FirstOrDefault(p => p.Id == id);
            blogVM.Categories = _context.Categories.ToList();
            blogVM.Tags = _context.Tags.ToList();
            
            //var blogVM=_context.Blogs.FirstOrDefault(x => x.Id == id);
            //var blogs=_context.Blogs.AsNoTracking().ToList();
            //if (id == null) return BadRequest();
            //if (blogs.Exists(p => p.Id == id))
            //{
            //    return View(blogs.Find(p => p.Id == id));
            //}

            // return BadRequest();
            return View(blogVM);
            

            
        }
       
        public IActionResult Event()
        {
            EventVM eventVM = new EventVM();
           
            eventVM.Events = _context.Events
                .Include(e => e.EventSpeakers)
                .ThenInclude(e => e.Speaker)
                .ToList();
            return View(eventVM);
        }
        public IActionResult EventDetails(int? id)
        {
            EventVM eventVM = new EventVM();
            eventVM.Tags = _context.Tags.ToList();
            eventVM.Speakers = _context.Speakers.ToList();
            eventVM.Event = _context.Events
                .Include(e => e.EventSpeakers)
                .ThenInclude(e => e.Speaker)
                .FirstOrDefault(p => p.Id == id);
            return View(eventVM);
        }
        public IActionResult Teacher()
        {
            HomeVM homeVM=new HomeVM();
            homeVM.Teachers = _context.Teachers
                .Include(c => c.Category)
                .Include(c => c.License)
                .ToList();
            homeVM.Categories = _context.Categories.ToList();
            homeVM.Tags = _context.Tags.ToList();

            return View(homeVM);
        }
        public IActionResult TeacherDetails(int?id)
        {
            TeacherVM teacherVM = new TeacherVM();
            teacherVM.Blogs=_context.Blogs.Take(3).ToList();
            teacherVM.Teacher = _context.Teachers
                .Include(c => c.Category)
                .Include(c => c.License)
                .FirstOrDefault(c => c.Id == id);
            teacherVM.Categories = _context.Categories.ToList();
            teacherVM.Licenses=_context.Licenses.ToList();
            return View(teacherVM);
        }
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contact(ContactVM contactVM)
        {
            Contact contact = new();
            contact.Name = contactVM.Name;
            contact.Email = contactVM.Email;
            contact.Subject = contactVM.Subject;
            contact.Message = contactVM.Message;
            _context.Contacts.Add(contact);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        

    }
}