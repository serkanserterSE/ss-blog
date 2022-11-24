using SS.Blog.Models.Dtos;

namespace SS.Blog.Services.Abstractions
{
    public interface IBlogOperations
    {
        Task AddBlog(BlogDto blog, CancellationToken cancellationToken);
        Task UpdateBlog(BlogDto blog, CancellationToken cancellationToken);
        Task RemoveBlog(Guid blogId, CancellationToken cancellationToken);
        Task<Dictionary<Guid, string>> GetBlogs(CancellationToken cancellationToken);
        Task<Dictionary<Guid, string>> GetBlogs(List<Guid> owners, CancellationToken cancellationToken);
        Task<Dictionary<Guid, string>> GetBlogsByCategories(List<Guid> categories, CancellationToken cancellationToken);
        Task<BlogDto> GetBlog(Guid blogId, CancellationToken cancellationToken);
    }
}
