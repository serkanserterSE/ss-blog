using SS.Blog.DataAccesses.Enums;

namespace SS.Blog.DataAccesses.Entities.Base
{
    public class BaseEntity
    {
        public long Id { get; set; }
        public Guid KeyId { get; set; }
        public DateTime CreatedOn { get; set; }
        public ERowStatus RowStatus { get; set; }
    }
}