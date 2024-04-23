using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduHome.Models
{
    public class Board
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [NotMapped,MaxLength(100)]
        public string? ShortDesc => Description.Length > 10 ? Description.Substring(0, 100) + "..." : Description;
    }
}
