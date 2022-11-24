using SS.Blog.DataAccesses.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SS.Blog.DataAccesses.Entities
{
    public class BlogContent : BaseEntity
    {
        [MaxLength(256)]
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public byte[] Content { get; set; }
        public long? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}
