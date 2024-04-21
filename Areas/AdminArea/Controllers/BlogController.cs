using EduHome.Areas.AdminArea.Views.ViewModels.Blog;
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
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;

        public BlogController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var datas = _context.Blogs.AsNoTracking().ToList();
            return View(datas);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(BlogCreateVM createVM)
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
            var blog = new Blog() { ImageUrl = photos.SaveFile("img/blog"), Title = createVM.Title, Description = createVM.Description, Author = createVM.Author, CreatedDate = createVM.CreatedDate };
            _context.Blogs.Add(blog);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int? id)
        {
            if (id is null) return NotFound();
            var existBlog = _context.Blogs.FirstOrDefault(s => s.Id == id);
            if (existBlog == null) return NotFound();
            BlogUpdateVM blogUpdateVM = new BlogUpdateVM();
            blogUpdateVM = new BlogUpdateVM { ImageUrl = existBlog.ImageUrl, Title = existBlog.Title, Description = existBlog.Description, Author = existBlog.Author, CreatedDate = existBlog.CreatedDate };
            return View(blogUpdateVM);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Update(int? id, BlogUpdateVM updateVM)
        {
            if (id is null) return NotFound();
            var existBlog = _context.Blogs.FirstOrDefault(s => s.Id == id);
            if (existBlog == null) return NotFound();
            var photo = updateVM.Photo;
            updateVM.ImageUrl = existBlog.ImageUrl;

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


                string fileName = photo.SaveFile("img/blog");
                DeleteFileHelper.DeleteFile("img/blog", existBlog.ImageUrl);
                existBlog.ImageUrl = fileName;

            };

            existBlog.Description = updateVM.Description;
            existBlog.Author = updateVM.Author;
            existBlog.CreatedDate = updateVM.CreatedDate;
            existBlog.Title = updateVM.Title;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            if (id is null) return NotFound();
            var existBlog = _context.Blogs.FirstOrDefault(s => s.Id == id);
            if (existBlog == null) return NotFound();
            return View(existBlog);
        }
        public IActionResult DeleteBlog(int? id)
        {
            if (id is null) return NotFound();
            var existBlog = _context.Blogs.FirstOrDefault(s => s.Id == id);
            if (existBlog == null) return NotFound();
            DeleteFileHelper.DeleteFile("img/blog", existBlog.ImageUrl);

            _context.Blogs.Remove(existBlog);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Detail(int? id)
        {
            if (id is null) return NotFound();
            var existBlog = _context.Blogs.FirstOrDefault(s => s.Id == id);
            if (existBlog == null) return NotFound();
            return View(existBlog);
        }
    }
}
