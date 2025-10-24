using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class FUNewsManagementDbContext : DbContext
    {
        public FUNewsManagementDbContext(DbContextOptions<FUNewsManagementDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<NewsArticle> NewsArticles { get; set; }
        public virtual DbSet<NewsTag> NewsTags { get; set; }
        public virtual DbSet<SystemAccount> SystemAccounts { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NewsTag>(entity =>
            {
                entity.HasKey(e => new { e.NewsArticleId, e.TagId });
            });

            modelBuilder.Entity<SystemAccount>(entity =>
            {
                entity.ToTable("SystemAccount");
                entity.Property(e => e.AccountId).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<NewsArticle>().ToTable("NewsArticle");
            modelBuilder.Entity<NewsTag>().ToTable("NewsTag");
            modelBuilder.Entity<Tag>().ToTable("Tag");
        }
    }
}