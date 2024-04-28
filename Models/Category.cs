using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EduHome.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [NotMapped, MaxLength(100)]
        public string? ShortDesc => Description.Length > 10 ? Description.Substring(0, 10) + "..." : Description;
        public List<Courses> Courses { get; set; }
        public List<Teacher> Teachers { get; set; }
        public List<Student> Students { get; set; }
    }
}
