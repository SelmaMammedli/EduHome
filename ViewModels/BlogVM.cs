using EduHome.Models;

namespace EduHome.ViewModels
{
    public class BlogVM
    {
        public IEnumerable<Blog> Blogs { get; set; }
        public Blog Blog { get; set; }
        public IEnumerable<Category> Categories { get; set; }

    }
}
