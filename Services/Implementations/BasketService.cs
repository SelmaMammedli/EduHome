using EduHome.DAL;
using EduHome.Services.Interfaces;
using EduHome.ViewModels.BasketVM;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace EduHome.Services.Implementations
{
    public class BasketService:IBasketService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly AppDbContext _context;
        public BasketService(IHttpContextAccessor contextAccessor, AppDbContext context)
        {
            _contextAccessor = contextAccessor;
            _context = context;

        }
        public int BasketCount()
        {
            var basket = _contextAccessor.HttpContext.Request.Cookies["basket"];
            if (basket == null) return 0;
            var courses = JsonConvert.DeserializeObject<List<BasketCourseVM>>(basket);
            return courses.Count;


        }

        public List<BasketCourseVM> GetCourses()
        {
            var basket = _contextAccessor.HttpContext.Request.Cookies["basket"];

            if (basket is null) return new List<BasketCourseVM>();
            var list = JsonConvert.DeserializeObject<List<BasketCourseVM>>(basket);
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
            return list;

        }
    }
}
