using EduHome.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EduHome.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<NoticeBoard> NoticesBoards { get; set;}
        public DbSet<Board> Boards { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<WhyYouChoose>WhyYouChooses { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<Welcome> Welcomes { get; set; }
        public DbSet<License>Licenses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Language>Languages { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subscribe> Subscriptions { get; set; }
    }
}
