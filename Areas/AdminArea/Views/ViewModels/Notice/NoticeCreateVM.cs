using System.ComponentModel.DataAnnotations.Schema;

namespace EduHome.Areas.AdminArea.Views.ViewModels.Notice
{
    public class NoticeCreateVM
    {
        public DateTime Time { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public string? ShortTitle => Description.Length > 10 ? Description.Substring(0, 10) + "..." : Description;
    }
}
