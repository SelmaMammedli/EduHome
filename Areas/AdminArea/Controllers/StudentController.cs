using EduHome.Areas.AdminArea.Views.ViewModels.Course;
using EduHome.Areas.AdminArea.Views.ViewModels.Student;
using EduHome.DAL;
using EduHome.Extensions;
using EduHome.Helper;
using EduHome.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class StudentController : Controller
    {
        private readonly AppDbContext _context;
        public StudentController(AppDbContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            var datas = _context.Students
                .Include(s => s.Category)
                .AsNoTracking()
                .ToList();
            return View(datas);
        }
        public IActionResult Create()
        {
            ViewBag.Category = _context.Categories.ToList();
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(StudentCreateVM createVM)
        {
            ViewBag.Category = _context.Categories.ToList();
            if (!ModelState.IsValid) return View();
            var photos = createVM.Photos;
            Student student = new Student();
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

                student.ImageUrl = photo.SaveFile("img/teacher");


            }
            student.About = createVM.About;
            student.Phone = createVM.Phone;
            student.Email = createVM.Email;
            student.FullName = createVM.FullName;
            student.CategoryId = createVM.CategoryId;


            _context.Students.Add(student);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int? id)
        {
            ViewBag.Category = _context.Categories.ToList();
            if (id == null) return NotFound();
            var student = _context.Students
                .Include(c => c.Category)
                .AsNoTracking()
                .FirstOrDefault(c => c.Id == id);
            if (student == null) return NotFound();
            StudentUpdateVM studentUpdateVM = new StudentUpdateVM();
            studentUpdateVM.ImageUrl = student.ImageUrl;
            studentUpdateVM.Email = student.Email;
            studentUpdateVM.FullName = student.FullName;
            studentUpdateVM.CategoryId =student.CategoryId;
            studentUpdateVM.Phone = student.Phone;
            studentUpdateVM.About = student.About;
            return View(studentUpdateVM);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Update(int? id, StudentUpdateVM updateVM)
        {
            ViewBag.Category = _context.Categories.ToList();
            if (id == null) return NotFound();
            var student = _context.Students
                .Include(c => c.Category)
                .FirstOrDefault(c => c.Id == id);
            if (student == null) return NotFound();
            var photos = updateVM.Photos;
            updateVM.ImageUrl = student.ImageUrl;
            if (photos != null && photos.Length > 0)
            {

                foreach (var photo in photos)
                {

                    if (!photo.CheckFile())
                    {
                        ModelState.AddModelError("Photo", "Please upload right file");
                        return View(updateVM);
                    }
                    if (photo.CheckSize(1000))
                    {
                        ModelState.AddModelError("Photo", "Please choose normal file");
                        return View(updateVM);
                    }
                    string fileName = photo.SaveFile("img/teacher");
                    DeleteFileHelper.DeleteFile("img/teacher", student.ImageUrl);
                    student.ImageUrl = fileName;
                }


            };
            student.Phone = updateVM.Phone;
            student.Email = updateVM.Email;
            student.About = updateVM.About;
            student.CategoryId = updateVM.CategoryId;
            student.FullName = updateVM.FullName;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            if (id is null) return NotFound();
            var existStudent = _context.Students
                .Include(c => c.Category)
                .FirstOrDefault(c => c.Id == id);
            if (existStudent == null) return NotFound();
            return View(existStudent);
        }
        public IActionResult DeleteStudent(int? id)
        {
            if (id is null) return NotFound();
            var existStudent = _context.Students
                .FirstOrDefault(c => c.Id == id);
            if (existStudent == null) return NotFound();

            DeleteFileHelper.DeleteFile("img/teacher", existStudent.ImageUrl);
            _context.Students.Remove(existStudent);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Detail(int? id)
        {
            if (id is null) return NotFound();
            var existStudent = _context.Students
                .Include(c => c.Category)
                .FirstOrDefault(c => c.Id == id);
            if (existStudent == null) return NotFound();
            return View(existStudent);
        }
    }
}
