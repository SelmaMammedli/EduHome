using EduHome.ViewModels.BasketVM;

namespace EduHome.Services.Interfaces
{
    public interface IBasketService
    {
        int BasketCount();
        List<BasketCourseVM> GetCourses();
    }
}
