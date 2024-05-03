using EduHome.DAL;
using EduHome.ViewModels.BasketVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace EduHome.Controllers
{
    public class BasketController : Controller
    {
        private readonly AppDbContext _context;
        public BasketController(AppDbContext context)
        {
            _context = context;

        }

        public IActionResult AddBasket(int? id)
        {
            if (id == null) return BadRequest();
            var course = _context.Courses
                .Include(p => p.Category)
                .FirstOrDefault(p => p.Id == id);
            if (course == null) return NotFound();
            List<BasketCourseVM> list;
            var basket = Request.Cookies["basket"];
            if (basket is null)
            {
                list = new();
            }
            else
            {
                list = JsonConvert.DeserializeObject<List<BasketCourseVM>>(basket);
            }
            var existCourseInBasket = list.Find(p => p.Id == id);
            if (existCourseInBasket is null)
            {
                BasketCourseVM basketCourseVM = new()
                {
                    Id = course.Id,
                    //Name = product.Name,
                    //Price = product.Price,
                    //CategoryName = product.Category.Name,
                    //MainImageUrl = product.ProductImages.Any(p => p.IsMain) ?
                    // product.ProductImages.FirstOrDefault(p => p.IsMain).ImageUrl :
                    // product.ProductImages.FirstOrDefault().ImageUrl,
                    BasketCount = 1
                };
                list.Add(basketCourseVM);
            }
            else
            {
                existCourseInBasket.BasketCount++;
            }

            var stringCourse = JsonConvert.SerializeObject(list);
            Response.Cookies.Append("basket", stringCourse);

            return RedirectToAction("Index", "Home");
        }
        public IActionResult ShowBasket()
        {
            var stringData = Request.Cookies["basket"];
            List<BasketCourseVM> list;
            if (stringData is null)
            {
                list = new();
            }
            else
            {
                list = JsonConvert.DeserializeObject<List<BasketCourseVM>>(stringData);
                foreach (var basketCourse in list)
                {
                    var existedCourse = _context.Courses
                        .Include(p => p.Category)
                        .FirstOrDefault(p => p.Id == basketCourse.Id);
                    if (existedCourse is not null)
                    {
                        basketCourse.Name = existedCourse.Category.Title;
                        basketCourse.Price = existedCourse.Price;
                        basketCourse.ImageUrl= existedCourse.ImageUrl;

                    }
                }
            }

            return View(list);
        }
        public IActionResult Increase(int? id)
        {
            if (id == null) return BadRequest();
            var course = _context.Courses
                .Include(p => p.Category)
               
                .FirstOrDefault(p => p.Id == id);
            if (course == null) return NotFound();
            List<BasketCourseVM> list = new();
            var basket = Request.Cookies["basket"];

            list = JsonConvert.DeserializeObject<List<BasketCourseVM>>(basket);

            var existCourseInBasket = list.Find(p => p.Id == id);
            if (existCourseInBasket is null)
            {
                BasketCourseVM basketCourseVM = new()
                {
                    Id = course.Id,
                   
                    Price = course.Price,
                    Name = course.Category.Title,
                   ImageUrl= course.ImageUrl,
                    BasketCount = 1
                };
                list.Add(basketCourseVM);
            }
            else
            {
                existCourseInBasket.BasketCount++;
            }

            var stringCourse = JsonConvert.SerializeObject(list);
            Response.Cookies.Append("basket", stringCourse);

            return RedirectToAction("ShowBasket");

        }
        public IActionResult Decrease(int? id)
        {
            if (id == null) return BadRequest();
            var course = _context.Courses
                .Include(p => p.Category)
                
                .FirstOrDefault(p => p.Id == id);
            if (course == null) return NotFound();
            List<BasketCourseVM> list = new();
            var basket = Request.Cookies["basket"];

            list = JsonConvert.DeserializeObject<List<BasketCourseVM>>(basket);

            var existCourseInBasket = list.Find(p => p.Id == id);
            if (existCourseInBasket is null)
            {
                BasketCourseVM basketCourseVM = new()
                {
                    Id = course.Id,
                    Name = course.Category.Title,
                    Price = course.Price,
                    ImageUrl= course.ImageUrl,
                    BasketCount = 1
                };
                list.Add(basketCourseVM);
            }
            else
            {
                existCourseInBasket.BasketCount--;
                if (existCourseInBasket.BasketCount == 0)
                {
                    var existCourse = list.FirstOrDefault(p => p.Id == id);
                    list.Remove(existCourse);
                }
            }

            var stringCourse = JsonConvert.SerializeObject(list);
            Response.Cookies.Append("basket", stringCourse);

            return RedirectToAction("ShowBasket");
        }
        public IActionResult Remove(int? id)
        {
            var stringData = Request.Cookies["basket"];
            List<BasketCourseVM> list = new();

            list = JsonConvert.DeserializeObject<List<BasketCourseVM>>(stringData);
            var existCourse = list.FirstOrDefault(p => p.Id == id);
            list.Remove(existCourse);

            var stringCourse = JsonConvert.SerializeObject(list);
            Response.Cookies.Append("basket", stringCourse);



            return RedirectToAction("ShowBasket");
        }
    }
}
