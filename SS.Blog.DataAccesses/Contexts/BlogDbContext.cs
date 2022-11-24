using Microsoft.EntityFrameworkCore;
using SS.Blog.DataAccesses.Entities;
using SS.Blog.DataAccesses.Entities.Base;
using SS.Blog.DataAccesses.Enums;

namespace SS.Blog.DataAccesses.Contexts
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("blog");
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<BlogContent> BlogContents { get; set; }

        public override int SaveChanges()
        {
            EntityChanges();
            return base.SaveChanges();
        }
        private void EntityChanges()
        {
            var deleteEntities = ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted);
            var updateEntities = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified);
            var otherEntities = ChangeTracker.Entries().Where(e => e.State != EntityState.Modified || e.State != EntityState.Deleted);

            if (deleteEntities.Count() > 0)
            {
                foreach (var entity in deleteEntities)
                {
                    if (entity.Entity is BaseEntity)
                    {
                        entity.State = EntityState.Modified;
                        var deletedEntity = entity.Entity as BaseEntity;
                        deletedEntity.RowStatus = ERowStatus.Deleted;
                    }
                }
            }
            else if (updateEntities.Count() > 0)
            {
                foreach (var entity in updateEntities)
                {
                    if (entity.Entity is BaseEntity)
                    {
                        var modifiedEntity = entity.Entity as BaseEntity;
                        modifiedEntity.RowStatus = ERowStatus.Modified;
                    }
                }
            }
            else
            {
                foreach (var entity in otherEntities)
                {
                    if (entity.Entity is BaseEntity)
                    {
                        var otherEntity = entity.Entity as BaseEntity;
                        otherEntity.RowStatus = ERowStatus.Active;
                    }
                }
            }
        }
    }
}
