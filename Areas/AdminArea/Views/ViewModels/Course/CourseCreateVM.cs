using System.ComponentModel.DataAnnotations.Schema;

namespace EduHome.Areas.AdminArea.Views.ViewModels.Course
{
    public class CourseCreateVM
    {
        public IFormFile[] Photos { get; set; }
        public int CategoryId { get; set; }
        public string AboutCourse { get; set; }
       
        public string AboutCourseTitle { get; set; }
        
        public string HowtoApply { get; set; }
    
        public string HowtoApplyTitle { get; set; }
       
        public string Certification { get; set; }
    
        public DateTime Starts { get; set; }
        public int Duration { get; set; }
        public int ClassDuration { get; set; }
        public int LanguageId { get; set; }
       
        public int Price { get; set; }
    }
}
