using EduHome.Areas.AdminArea.Views.ViewModels.Course;
using EduHome.DAL;
using EduHome.Extensions;
using EduHome.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class CoursesController : Controller
    {
        private readonly AppDbContext _context;

        public CoursesController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var datas=_context.Courses
                .Include(c=>c.Category)
                .AsNoTracking()
                .ToList();
            return View(datas);
        }
        public IActionResult Create()
        {
            ViewBag.Category=_context.Categories.ToList();
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(CourseCreateVM createVM)
        {
            ViewBag.Category = _context.Categories.ToList();
            if (!ModelState.IsValid) return View();

            var photos = createVM.Photos;
            Courses newCourses = new Courses();
            foreach (var photo in photos)
            {
                if (photo == null || photo.Length == 0)
                {
                    ModelState.AddModelError("Photo", "Please upload file");
                    return View();
                }
                if (!photo.CheckFile())
                {
                    ModelState.AddModelError("Photo", "Please upload right file");
                    return View();
                }
                if (photo.CheckSize(1000))
                {
                    ModelState.AddModelError("Photo", "Please choose normal file");
                    return View();
                }
           
                newCourses.ImageUrl = photo.SaveFile("img/course");
            
                


               // newProduct.ProductImages.Add(productImage);
            }
         
            newCourses.CategoryId = createVM.CategoryId;
            newCourses.Price = createVM.Price;
            newCourses.AboutCourse = createVM.AboutCourse;
            newCourses.AboutCourseTitle = createVM.AboutCourseTitle;
            newCourses.Duration = createVM.Duration;
            newCourses.ClassDuration = createVM.ClassDuration;
            newCourses.Certification = createVM.Certification;
            newCourses.Language = createVM.Language;
            newCourses.HowtoApply = createVM.HowtoApply;
            newCourses.HowtoApplyTitle = createVM.HowtoApplyTitle;
            newCourses.Starts=createVM.Starts;

            _context.Courses.Add(newCourses);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
