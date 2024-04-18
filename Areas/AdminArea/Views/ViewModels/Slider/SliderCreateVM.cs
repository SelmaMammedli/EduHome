using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduHome.Areas.AdminArea.Views.ViewModels.Slider
{
    public class SliderCreateVM
    {
        public string Title { get; set; }
        public string Descrription { get; set; }
        public string ImageUrl { get; set; }

       [Required(ErrorMessage = "PLEASE FILL")]
        public IFormFile Photo { get; set; }
    }
}
