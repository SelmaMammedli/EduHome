using System.ComponentModel.DataAnnotations.Schema;

namespace EduHome.Models
{
    public class NoticeBoard
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public string? ShortTitle => Description.Length > 10 ? Description.Substring(0, 10) + "..." : Description;
    }
}
