using System.ComponentModel.DataAnnotations;

namespace EduHome.Areas.AdminArea.Views.ViewModels.Slider
{
    public class SliderUpdateVM
    {
        public string Title { get; set; }
        public string Descrription { get; set; }
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "PLEASE FILL")]
        public IFormFile Photo { get; set; }
    }
}
