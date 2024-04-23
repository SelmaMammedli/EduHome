﻿using EduHome.Models;

namespace EduHome.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Slider> Sliders { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
        public IEnumerable<NoticeBoard> NoticeBoards { get; set; }
        public IEnumerable<Board> Boards { get; set; }

    }
}
