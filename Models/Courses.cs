using System.ComponentModel.DataAnnotations.Schema;

namespace EduHome.Models
{
    public class Courses
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string AboutCourse { get; set; }
        [NotMapped]
        public string? ShortAC => AboutCourse.Length > 10 ? AboutCourse.Substring(0, 4) + "..." : AboutCourse;
        public string AboutCourseTitle { get; set; }
        [NotMapped]
        public string? ShortACT => AboutCourseTitle.Length > 10 ? AboutCourseTitle.Substring(0, 4) + "..." : AboutCourseTitle;
        public string HowtoApply { get; set; }
        [NotMapped]
        public string? ShortHA => HowtoApply.Length > 10 ? HowtoApply.Substring(0, 5) + "..." : HowtoApply;
        public string HowtoApplyTitle { get; set; }
        [NotMapped]
        public string? ShortHAT => HowtoApplyTitle.Length > 10 ? HowtoApplyTitle.Substring(0, 5) + "..." : HowtoApplyTitle;
        public string Certification { get; set; }
        [NotMapped]
        public string? ShortCerc => Certification.Length > 10 ? Certification.Substring(0, 5) + "..." : Certification;
        public DateTime Starts { get; set; }
        public int Duration { get; set; }
        public int ClassDuration { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; }
        public int Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
