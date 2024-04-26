namespace EduHome.Models
{
    public class License
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Teacher> Teachers { get; set; }
    }
}
