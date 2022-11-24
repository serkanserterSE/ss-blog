namespace SS.Blog.Models.Dtos
{
    public class CategoryDto
    {
        public Guid? KeyId { get; set; }
        public string Name { get; set; }
        public Guid? TopCategoryKeyId { get; set; }
        public CategoryDto? TopCategory { get; set; } = null;
    }
}
