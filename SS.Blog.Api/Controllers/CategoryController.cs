using Microsoft.AspNetCore.Mvc;
using SS.Blog.Models.Dtos;
using SS.Blog.Services.Abstractions;

namespace SS.Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryOperations _categoryOperations;

        public CategoryController(ICategoryOperations categoryOperations)
        {
            _categoryOperations = categoryOperations;
        }

        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryDto categoryDto)
        {
            return Ok(_categoryOperations.AddCategory(categoryDto, CancellationToken.None));
        }

        [HttpPost("RemoveCategory")]
        public async Task<IActionResult> RemoveCategory([FromBody] Guid keyId)
        {
            await _categoryOperations.RemoveCategory(keyId, CancellationToken.None);
            return Ok();
        }

        [HttpGet("GetCategory")]
        public async Task<IActionResult> GetCategory(Guid categoryKeyId)
        {
            return Ok(await _categoryOperations.GetCategory(categoryKeyId, CancellationToken.None));
        }
    }
}
