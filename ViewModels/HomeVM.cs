﻿using EduHome.Models;

namespace EduHome.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Slider> Sliders { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
        public IEnumerable<NoticeBoard> NoticeBoards { get; set; }
        public IEnumerable<Board> Boards { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Courses> Courses { get; set; }
        public IEnumerable<Teacher> Teachers { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public IEnumerable<Student> Students { get; set; }
        public IEnumerable<Event>Events { get; set; }
        public Subscribe Subscribe { get; set; }
        public WhyYouChoose WhyYouChoose { get; set; }
        public Welcome Welcome { get; set; }


    }
}
