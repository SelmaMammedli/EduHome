using System.ComponentModel.DataAnnotations.Schema;

namespace EduHome.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [NotMapped]
        public string? ShortTitle => Title.Length > 10 ? Title.Substring(0, 10) + "..." : Title;
        public string Description { get; set; }
        [NotMapped]
        public string? ShortDesc => Description.Length > 10 ? Description.Substring(0, 10) + "..." : Description;
        public string Author { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ImageUrl { get; set; }
    }
}
