namespace SS.Blog.Models.Dtos
{
    public class BlogDto
    {
        public Guid KeyId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public string? CategoryName{ get; set; }
        public Guid? CategoryKeyId{ get; set; }
    }
}
