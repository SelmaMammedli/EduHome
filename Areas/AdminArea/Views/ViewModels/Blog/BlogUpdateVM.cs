﻿using System.ComponentModel.DataAnnotations;

namespace EduHome.Areas.AdminArea.Views.ViewModels.Blog
{
    public class BlogUpdateVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "PLEASE FILL")]
        public IFormFile Photo { get; set; }
    }
}
