using SS.Blog.Models.Dtos;

namespace SS.Blog.Services.Abstractions
{
    public interface ICategoryOperations
    {
        Task AddCategory(CategoryDto category, CancellationToken cancellationToken);
        Task UpdateCategory(CategoryDto category, CancellationToken cancellationToken);
        Task RemoveCategory(Guid categoryKeyId, CancellationToken cancellationToken);
        Task<CategoryDto> GetCategory(Guid categoryKeyId, CancellationToken cancellationToken);
        Task<List<CategoryDto>> GetSubCategories(Guid categoryKeyId, CancellationToken cancellationToken);
        Task<CategoryDto> GetTopCategory(Guid categoryKeyId, CancellationToken cancellationToken);
    }
}
