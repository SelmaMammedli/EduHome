using EduHome.Areas.AdminArea.Views.ViewModels.Course;
using EduHome.Areas.AdminArea.Views.ViewModels.Teacher;
using EduHome.DAL;
using EduHome.Extensions;
using EduHome.Helper;
using EduHome.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class TeacherController : Controller
    {
        private readonly AppDbContext _context;
        public TeacherController(AppDbContext context)
        {
            _context = context;
            
        }
        public IActionResult Index()
        {
            var datas=_context.Teachers
                .Include(t=>t.Category)
                .Include(t=>t.License)
                .AsNoTracking()
                .ToList();
            return View(datas);
        }
        public IActionResult Create()
        {
            ViewBag.Category = _context.Categories.ToList();
            ViewBag.License=_context.Licenses.ToList();
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(TeacherCreateVM createVM)
        {
            ViewBag.Category = _context.Categories.ToList();
            ViewBag.License = _context.Licenses.ToList();
            if (!ModelState.IsValid) return View();

            var photos = createVM.Photos;
            Teacher newTeacher=new Teacher();

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

                newTeacher.ImageUrl = photo.SaveFile("img/teacher");

            }
            newTeacher.CategoryId=createVM.CategoryId;
            newTeacher.LicenseId=createVM.LicenseId;
            newTeacher.FullName = createVM.FullName;
            newTeacher.AboutMe = createVM.AboutMe;
            newTeacher.Experience = createVM.Experience;
            newTeacher.Hobbies = createVM.Hobbies;
            newTeacher.Faculty = createVM.Faculty;
            newTeacher.Degree = createVM.Degree;
            newTeacher.Mail = createVM.Mail;
            newTeacher.Phone = createVM.Phone;
            newTeacher.Linkedin = createVM.Linkedin;
            newTeacher.Instagram = createVM.Instagram;
            newTeacher.Facebook = createVM.Facebook;
            newTeacher.Twitter = createVM.Twitter;
            newTeacher.LanguagePercent = createVM.LanguagePercent;
            newTeacher.Leadership = createVM.Leadership;
            newTeacher.Development = createVM.Development;
            newTeacher.Communication = createVM.Communication;
            newTeacher.Innovation = createVM.Innovation;
            newTeacher.Researching = createVM.Researching;
            _context.Teachers.Add(newTeacher);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int? id)
        {
            ViewBag.Category = _context.Categories.ToList();
            ViewBag.License = _context.Licenses.ToList();
            if (id == null) return NotFound();
            var teacher = _context.Teachers
                .Include(c => c.Category)
                .Include(c => c.License)
                .AsNoTracking()
                .FirstOrDefault(c => c.Id == id);
            if (teacher == null) return NotFound();


            TeacherUpdateVM teacherUpdateVM = new TeacherUpdateVM();
            teacherUpdateVM.CategoryId= teacher.CategoryId;
            teacherUpdateVM.LicenseId= teacher.LicenseId;
            teacherUpdateVM.ImageUrl = teacher.ImageUrl;
            teacherUpdateVM.FullName = teacher.FullName;
            teacherUpdateVM.AboutMe= teacher.AboutMe;
            teacherUpdateVM.Experience = teacher.Experience;
            teacherUpdateVM.Hobbies = teacher.Hobbies;
            teacherUpdateVM.Faculty = teacher.Faculty;
            teacherUpdateVM.Degree = teacher.Degree;
            teacherUpdateVM.Mail = teacher.Mail;
            teacherUpdateVM.Phone = teacher.Phone;
            teacherUpdateVM.Instagram = teacher.Instagram;
            teacherUpdateVM.Linkedin = teacher.Linkedin;
            teacherUpdateVM.Facebook = teacher.Facebook;
            teacherUpdateVM.Twitter = teacher.Twitter;
            teacherUpdateVM.Leadership = teacher.Leadership;
            teacherUpdateVM.LanguagePercent = teacher.LanguagePercent;
            teacherUpdateVM.Development= teacher.Development;
            teacherUpdateVM.Communication = teacher.Communication;
            teacherUpdateVM.Innovation = teacher.Innovation;
            teacherUpdateVM.Researching = teacher.Researching;


            return View(teacherUpdateVM);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Update(int? id, TeacherUpdateVM teacherUpdateVM)
        {
            ViewBag.Category = _context.Categories.ToList();
            ViewBag.License = _context.Licenses.ToList();
            if (id == null) return NotFound();
            var teacher = _context.Teachers
                .Include(c => c.Category)
                .Include(c => c.License)
                .FirstOrDefault(c => c.Id == id);
            if (teacher == null) return NotFound();

            var photos = teacherUpdateVM.Photos;

            teacherUpdateVM.ImageUrl = teacher.ImageUrl;
            if (photos != null && photos.Length > 0)
            {

                foreach (var photo in photos)
                {

                    if (!photo.CheckFile())
                    {
                        ModelState.AddModelError("Photo", "Please upload right file");
                        return View(teacherUpdateVM);
                    }
                    if (photo.CheckSize(1000))
                    {
                        ModelState.AddModelError("Photo", "Please choose normal file");
                        return View(teacherUpdateVM);
                    }
                    string fileName = photo.SaveFile("img/teacher");
                    DeleteFileHelper.DeleteFile("img/teacher", teacher.ImageUrl);
                    teacher.ImageUrl = fileName;
                }


            };
             teacher.CategoryId=teacherUpdateVM.CategoryId;
             teacher.LicenseId=teacherUpdateVM.LicenseId;
             
            teacher.FullName = teacherUpdateVM.FullName;
            teacher.AboutMe = teacherUpdateVM.AboutMe;
            teacher.Experience = teacherUpdateVM.Experience;
            teacher.Hobbies = teacherUpdateVM.Hobbies;
            teacher.Faculty = teacherUpdateVM.Faculty;
            teacher.Degree = teacherUpdateVM    .Degree;
            teacher.Mail = teacherUpdateVM.Mail;
            teacher.Phone = teacherUpdateVM.Phone;
            teacher.Instagram = teacherUpdateVM.Instagram;
            teacher.Linkedin = teacherUpdateVM.Linkedin;
            teacher.Facebook = teacherUpdateVM.Facebook;
            teacher.Twitter = teacherUpdateVM.Twitter;
            teacher.Leadership = teacherUpdateVM.Leadership;
            teacher.LanguagePercent = teacherUpdateVM.LanguagePercent;
            teacher.Development = teacherUpdateVM.Development;
            teacher.Communication = teacherUpdateVM.Communication;
            teacher.Innovation = teacherUpdateVM.Innovation;
            teacher.Researching = teacherUpdateVM.Researching;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            if (id is null) return NotFound();
            var existTeacher=_context.Teachers
                .Include(c => c.Category)
                .Include(c=>c.License)
                .FirstOrDefault(c => c.Id == id);
            if(existTeacher==null)return NotFound();
            return View(existTeacher);
        }
        public IActionResult DeleteTeacher(int? id)
        {
            if (id is null) return NotFound();
            var existTeacher = _context.Teachers
                .FirstOrDefault(c => c.Id == id);
            if (existTeacher == null) return NotFound();
            DeleteFileHelper.DeleteFile("img/teacher", existTeacher.ImageUrl);
            _context.Teachers.Remove(existTeacher);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Detail(int? id)
        {
            if (id is null) return NotFound();
            var existTeacher = _context.Teachers
                .Include(c => c.Category)
                .Include(c => c.License)
                .FirstOrDefault(c => c.Id == id);
            if (existTeacher == null) return NotFound();
            return View(existTeacher);
        }
    }
}
