using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EduHome.Areas.AdminArea.Views.ViewModels.Category
{
    public class CategoryUpdateVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        [NotMapped, MaxLength(100)]
        public string? ShortDesc => Description.Length > 10 ? Description.Substring(0, 10) + "..." : Description;
    }
}
