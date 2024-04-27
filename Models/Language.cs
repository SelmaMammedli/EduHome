namespace EduHome.Models
{
    public class Language
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Courses>Courses { get; set; }
    }
}
