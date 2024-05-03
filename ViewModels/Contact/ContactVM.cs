using EduHome.Models;

namespace EduHome.ViewModels.Contact
{
    public class ContactVM
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public IEnumerable<Location> Locations { get; set; }
    }
}
