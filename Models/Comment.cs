namespace EduHome.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public string Description { get; set; }
        public Category CategoryId { get; set; }
        public string ImageUrl { get; set; }
    }
}
