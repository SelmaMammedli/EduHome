
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
            homeVm.WhyYouChoose = _context.WhyYouChooses.FirstOrDefault();
            return View(homeVm);
        }
        public IActionResult Courses()
        {
            HomeVM homeVM = new HomeVM();
            homeVM.Courses= _context.Courses
                .Include(c=>c.Category)
                .ToList();
            return View(homeVM);
        }
        public IActionResult CoursesDetails(int? id)
        {
            CoursesVM coursesVM = new CoursesVM();
            coursesVM.Blog = _context.Blogs.FirstOrDefault(c => c.Id == id);
            coursesVM.Blogs=_context.Blogs.Take(3).ToList();
            coursesVM.Categories= _context.Categories.ToList();
            coursesVM.Courses = _context.Courses
                .Include(c => c.Category)
                .FirstOrDefault(c => c.Id == id);
            return View(coursesVM);
        }
        public IActionResult About()
        {
            HomeVM homeVm = new HomeVM();
            homeVm.NoticeBoards = _context.NoticesBoards.ToList();
            homeVm.Welcome = _context.Welcomes.FirstOrDefault();
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
            blogVM.Blogs = _context.Blogs.ToList();
            blogVM.Blog = _context.Blogs.FirstOrDefault(p => p.Id == id);
            blogVM.Categories = _context.Categories.ToList();
            
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
            return View();
        }
        public IActionResult Teacher()
        {
            return View();
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