namespace EduHome.Areas.AdminArea.Views.ViewModels.Student
{
    public class StudentUpdateVM
    {
        public string FullName { get; set; }
        public string About { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile[]? Photos { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int CategoryId { get; set; }
    }
}
