using EduHome.Models;

namespace EduHome.ViewModels
{
    public class CoursesVM
    {
        public IEnumerable<Blog> Blogs { get; set; }
        public Blog Blog { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public Courses Courses { get; set; }
        public Category Category { get; set; }
    }
}
