using SS.Blog.DataAccesses.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace SS.Blog.DataAccesses.Entities
{
    public class Category : BaseEntity
    { 
        [MaxLength(256)]
        public string Name { get; set; }
        public long? TopCategoryId { get; set; } = null;
    }
}
