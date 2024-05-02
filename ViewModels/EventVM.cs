using EduHome.Models;
using EduHome.ViewModels.Contact;

namespace EduHome.ViewModels
{
    public class EventVM
    {
        public IEnumerable<Event> Events { get; set; }
        public Event Event { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public ContactVM ContactVM { get; set; }
        public IEnumerable<Speaker> Speakers { get; set; }
    }
}
