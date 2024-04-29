namespace EduHome.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Venue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public List<EventSpeaker> EventSpeakers { get; set; }
    }
}
