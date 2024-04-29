namespace EduHome.Models
{
    public class Speaker
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string ImageUrl { get; set; }
        public string Degree { get; set; }
        public string Profession { get; set; }
        public List<EventSpeaker> EventSpeakers { get; set; }
    }
}
