using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EduHome.Models
{
    public class Bio
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        [NotMapped]
        public string? ShortValue => Value.Length > 10 ? Value.Substring(0, 10) + "..." : Value;
    }
}
