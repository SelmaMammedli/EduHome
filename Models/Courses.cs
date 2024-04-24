namespace EduHome.Models
{
    public class Courses
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string AboutCourse { get; set; }
        public string AboutCourseTitle { get; set; }
        public string HowtoApply { get; set; }
        public string HowtoApplyTitle { get; set; }
        public string Certification { get; set; }
        public DateTime Starts { get; set; }
        public int Duration { get; set; }
        public int ClassDuration { get; set; }
        public string Language { get; set; }
        public int Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
