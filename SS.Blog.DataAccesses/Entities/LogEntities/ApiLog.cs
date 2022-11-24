using SS.Blog.DataAccesses.Enums;
using System.ComponentModel.DataAnnotations;

namespace SS.Blog.DataAccesses.Entities.LogEntities
{
    public class ApiLog
    {
        public long Id { get; set; }
        public Guid KeyId { get; set; }
        public EMethod MethodType { get; set; }
        [MaxLength(32)]
        public string MethodTypeName { get; set; }
        public DateTime Date { get; set; }
        [MaxLength(2048)]
        public string RequestUrl { get; set; }
        [MaxLength(64)]
        public string RequestHttpType { get; set; }
        public string Content { get; set; }
    }
}
