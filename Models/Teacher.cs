using System.ComponentModel.DataAnnotations.Schema;

namespace EduHome.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int LicenseId { get; set; }
        public License License { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string AboutMe { get; set; }
        [NotMapped]
        public string? ShortAbout => AboutMe.Length > 10 ? AboutMe.Substring(0, 10) + "..." : AboutMe;
        public int Experience { get; set; }
        public string Hobbies { get; set; }
        public string Faculty { get; set; }
        public string Degree { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Linkedin { get; set; }
        public string Twitter { get; set; }
        public string ImageUrl { get; set; }
        public int LanguagePercent { get; set; }
        public int Leadership { get; set; }
        public int Development { get; set; }
        public int Communication { get; set; }
        public int Innovation { get; set; }
        public int Researching { get; set; }
      
    }
}
