namespace SS.Blog.Models.Dtos
{
    public class ApiLogDto
    {
        public Guid KeyId { get; set; }
        public DateTime Date { get; set; }
        public string RequestUrl { get; set; }
        public string RequestHttpType { get; set; }
        public string Content { get; set; }
    }
}
