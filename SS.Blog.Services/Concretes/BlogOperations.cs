using Microsoft.EntityFrameworkCore;
using SS.Blog.DataAccesses.Contexts;
using SS.Blog.DataAccesses.Entities;
using SS.Blog.Models.Dtos;
using SS.Blog.Services.Abstractions;

namespace SS.Blog.Services.Concretes
{
    public class BlogOperations : IBlogOperations
    {
        private readonly BlogDbContext _blogDbContext;
        public BlogOperations(BlogDbContext blogDbContext)
        {
            _blogDbContext = blogDbContext;
        }

        public async Task AddBlog(BlogDto blog, CancellationToken cancellationToken)
        {
            var category = new Category();
            if (blog.CategoryKeyId != null)
            {
                category = _blogDbContext.Categories.Where(p => p.KeyId == blog.CategoryKeyId).FirstOrDefault() ?? null;
            }
            var newBlog = new BlogContent()
            {
                KeyId = Guid.NewGuid(),
                PublishDate = blog.PublishDate,
                Title = blog.Title,
                CategoryId = category?.Id,
                Content = Convert.FromBase64String(blog.Content),
            };
            await _blogDbContext.BlogContents.AddAsync(newBlog);
            await _blogDbContext.SaveChangesAsync();
        }

        public async Task<BlogDto> GetBlog(Guid blogId, CancellationToken cancellationToken)
        {
            var blog = _blogDbContext.BlogContents.Where(p => p.KeyId == blogId).FirstOrDefault();
            if (blog == null) throw new Exception("Not Found");
            var category = new Category();
            if (blog.Category != null)
            {
                category = _blogDbContext.Categories.Where(p => p.Id == blog.Category.Id).FirstOrDefault() ?? null;
            }

            return new BlogDto()
            {
                Title = blog.Title,
                CategoryKeyId = category?.KeyId,
                CategoryName = category?.Name,
                PublishDate = blog.PublishDate,
                Content = Convert.ToBase64String(blog.Content),
                KeyId = blog.KeyId
            };
        }

        public async Task<Dictionary<Guid, string>> GetBlogs(CancellationToken cancellationToken)
        {
            return await _blogDbContext.BlogContents.ToDictionaryAsync(p => p.KeyId, p => p.Title);
        }

        public async Task<Dictionary<Guid, string>> GetBlogs(List<Guid> owners, CancellationToken cancellationToken)
        {
            //return await _blogDbContext.BlogContents.Where(p=>).ToDictionaryAsync(p => p.KeyId, p => p.Title);
            throw new NotImplementedException();
        }

        public async Task<Dictionary<Guid, string>> GetBlogsByCategories(List<Guid> categories, CancellationToken cancellationToken)
        {
            return await _blogDbContext.BlogContents.Where(p => categories.Contains(p.Category.KeyId)).ToDictionaryAsync(p => p.KeyId, p => p.Title);
        }

        public async Task RemoveBlog(Guid blogId, CancellationToken cancellationToken)
        {
            var blog = _blogDbContext.BlogContents.Where(p => p.KeyId == blogId).FirstOrDefault();
            if (blog == null) throw new Exception("Not Found");
            _blogDbContext.Remove(blog);
            _blogDbContext.SaveChanges();
        }

        public async Task UpdateBlog(BlogDto blog, CancellationToken cancellationToken)
        {
            var updateBlog = _blogDbContext.BlogContents.Where(p => p.KeyId == blog.KeyId).FirstOrDefault();
            if (updateBlog == null) throw new Exception("Not Found");
            var category = new Category();
            if (blog.CategoryKeyId != null)
            {
                category = _blogDbContext.Categories.Where(p => p.KeyId == blog.CategoryKeyId).FirstOrDefault() ?? null;
            }
            updateBlog.PublishDate = blog.PublishDate;
            updateBlog.CategoryId = category?.Id;
            updateBlog.Content = Convert.FromBase64String(blog.Content);
            updateBlog.Title = blog.Title;

            _blogDbContext.Update(updateBlog);
            _blogDbContext.SaveChanges();
        }
    }
}
