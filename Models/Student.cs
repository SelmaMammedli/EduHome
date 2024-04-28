using System.ComponentModel.DataAnnotations.Schema;

namespace EduHome.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string About { get; set; }
        [NotMapped]
        public string? ShortAbout => About.Length > 10 ? About.Substring(0, 4) + "..." : About;
        public string ImageUrl { get; set; }
        public string Email { get; set; }
        public string Phone {  get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
