using EduHome.Models;

namespace EduHome.ViewModels
{
    public class TeacherVM
    {
        public Teacher Teacher { get; set; }
        public IEnumerable<Teacher> Teachers { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<License> Licenses { get; set; }    
    }
}
