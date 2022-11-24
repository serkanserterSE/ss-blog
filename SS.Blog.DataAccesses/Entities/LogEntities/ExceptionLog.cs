using System.ComponentModel.DataAnnotations;

namespace SS.Blog.DataAccesses.Entities.LogEntities
{
    public class ExceptionLog
    {
        public long Id { get; set; }
        public Guid KeyId { get; set; }
        [MaxLength(512)]
        public string Url { get; set; }
        public DateTime Date { get; set; }
        [MaxLength(2048)]
        public string ExceptionMessage { get; set; }
    }
}
