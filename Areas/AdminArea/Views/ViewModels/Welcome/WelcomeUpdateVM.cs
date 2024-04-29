namespace EduHome.Areas.AdminArea.Views.ViewModels.Welcome
{
    public class WelcomeUpdateVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string VideoLink { get; set; }
        public IFormFile Photo { get; set; }
    }
}
