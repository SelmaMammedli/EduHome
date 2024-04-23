
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
            return View(homeVm);
        }
        public IActionResult Courses()
        {
            return View();
        }
        public IActionResult About()
        {
            HomeVM homeVm = new HomeVM();
            homeVm.NoticeBoards = _context.NoticesBoards.ToList();
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