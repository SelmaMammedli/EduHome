namespace EduHome.Areas.AdminArea.Views.ViewModels.Speaker
{
    public class SpeakerCreateVM
    {
       
        public string FullName { get; set; }
        public IFormFile Photo { get; set; }
        public string Degree { get; set; }
        public string Profession { get; set; }
    }
}
