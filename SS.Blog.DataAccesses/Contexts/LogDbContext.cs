using Microsoft.EntityFrameworkCore;
using SS.Blog.DataAccesses.Entities.LogEntities;

namespace SS.Blog.DataAccesses.Contexts
{
    public class LogDbContext : DbContext
    {
        public LogDbContext(DbContextOptions<LogDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("log");
        }

        public virtual DbSet<ApiLog> ApiLog { get; set; }
        public virtual DbSet<ExceptionLog> ExceptionLog { get; set; }
    }
}
