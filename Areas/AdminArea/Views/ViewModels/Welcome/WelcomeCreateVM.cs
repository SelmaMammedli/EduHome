﻿namespace EduHome.Areas.AdminArea.Views.ViewModels.Welcome
{
    public class WelcomeCreateVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string VideoLink { get; set; }
        public IFormFile Photo { get; set; }
    }
}
