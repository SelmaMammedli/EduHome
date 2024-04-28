namespace EduHome.Areas.AdminArea.Views.ViewModels.Student
{
    public class StudentCreateVM
    {
        public string FullName { get; set; }
        public string About { get; set; }
        public IFormFile[] Photos { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int CategoryId { get; set; }
    }
}
