using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EduHome.Models
{
    public class Welcome
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [NotMapped, MaxLength(100)]
        public string? ShortDesc => Description.Length > 10 ? Description.Substring(0, 10) + "..." : Description;
        public string VideoLink { get; set; }
        public string ImageUrl { get; set; }
    }
}
