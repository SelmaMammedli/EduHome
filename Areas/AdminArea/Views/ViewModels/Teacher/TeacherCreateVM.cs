namespace EduHome.Areas.AdminArea.Views.ViewModels.Teacher
{
    public class TeacherCreateVM
    {
        public IFormFile[] Photos { get; set; }
        public string FullName { get; set; }
        public int LicenseId { get; set; }
        public int CategoryId { get; set; }
        public string AboutMe { get; set; }
        public int Experience { get; set; }
        public string Hobbies { get; set; }
        public string Faculty { get; set; }
        public string Degree { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Linkedin { get; set; }
        public string Twitter { get; set; }
        
        public int LanguagePercent { get; set; }
        public int Leadership { get; set; }
        public int Development { get; set; }
        public int Communication { get; set; }
        public int Innovation { get; set; }
        public int Researching { get; set; }
    }
}
