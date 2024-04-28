using EduHome.Models;
using EduHome.ViewModels.Contact;

namespace EduHome.ViewModels
{
    public class BlogVM
    {
        public IEnumerable<Blog> Blogs { get; set; }
        public Blog Blog { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public ContactVM ContactVM { get; set; }
        public IEnumerable<Tag> Tags { get; set; }

    }
}
