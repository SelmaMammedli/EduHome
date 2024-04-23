using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EduHome.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        [NotMapped, MaxLength(100)]
        public string? ShortMesg => Message.Length > 10 ? Message.Substring(0, 10) + "..." : Message;
    }
}
