using SS.Blog.DataAccesses.Contexts;
using SS.Blog.DataAccesses.Entities;
using SS.Blog.DataAccesses.Enums;
using SS.Blog.Models.Dtos;
using SS.Blog.Services.Abstractions;

namespace SS.Blog.Services.Concretes
{
    public class CategoryOperations : ICategoryOperations
    {
        private readonly BlogDbContext _blogDbContext;

        public CategoryOperations(BlogDbContext blogDbContext)
        {
            _blogDbContext = blogDbContext;
        }

        public async Task AddCategory(CategoryDto category, CancellationToken cancellationToken)
        {
            Category topCatecory = null;
            if (category.TopCategoryKeyId != null)
            {
                topCatecory = _blogDbContext.Categories.Where(p => p.KeyId == category.TopCategoryKeyId).FirstOrDefault();
            }
            var newCategory = new Category()
            {
                KeyId = Guid.NewGuid(),
                Name = category.Name,
                CreatedOn = DateTime.Now,
                TopCategoryId = topCatecory?.Id,
                RowStatus = ERowStatus.Active
            };

            _blogDbContext.Categories.Add(newCategory);
            _blogDbContext.SaveChanges();
        }

        public async Task<CategoryDto> GetCategory(Guid categoryKeyId, CancellationToken cancellationToken)
        {
            var currentCategory = _blogDbContext.Categories.Where(p => p.KeyId == categoryKeyId).FirstOrDefault();
            var categortDto = new CategoryDto()
            {
                Name = currentCategory.Name,
                KeyId = currentCategory.KeyId
            };
            GetTopCategory(currentCategory, categortDto);
            return categortDto;
        }

        private Category GetTopCategory(Category category, CategoryDto categoryDto)
        {
            if (category.TopCategoryId == null)
                return category;
            var currentTopCategory = _blogDbContext.Categories.Where(p => p.Id == category.TopCategoryId).FirstOrDefault();
            var currentTopCategoryDto = new CategoryDto()
            {
                Name = currentTopCategory.Name,
                KeyId = currentTopCategory.KeyId
            };
            categoryDto.TopCategoryKeyId = currentTopCategory.KeyId;
            categoryDto.TopCategory = currentTopCategoryDto;
            return GetTopCategory(currentTopCategory, currentTopCategoryDto);
        }

        public async Task<List<CategoryDto>> GetSubCategories(Guid categoryKeyId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<CategoryDto> GetTopCategory(Guid categoryKeyId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveCategory(Guid categoryKeyId, CancellationToken cancellationToken)
        {
            var category = _blogDbContext.Categories.FirstOrDefault(p => p.KeyId == categoryKeyId);
            _blogDbContext.Categories.Remove(category);
            _blogDbContext.SaveChanges();
        }

        public async Task UpdateCategory(CategoryDto category, CancellationToken cancellationToken)
        {
            var updateCategory = _blogDbContext.Categories.FirstOrDefault(p => p.KeyId == category.KeyId);
            if (updateCategory == null) throw new Exception("Not Found!");

            Category topCatecory = null;
            if (category.TopCategory != null)
            {
                topCatecory = _blogDbContext.Categories.Where(p => p.KeyId == category.TopCategory.KeyId).FirstOrDefault();
            }

            updateCategory.Name = category.Name;
            updateCategory.TopCategoryId = topCatecory?.Id;

            _blogDbContext.Categories.Update(updateCategory);
            _blogDbContext.SaveChanges();
        }
    }
}
