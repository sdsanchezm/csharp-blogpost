using Microsoft.EntityFrameworkCore;
using blogpost.Models;


namespace blogpost.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) { }


        public DbSet<BlogPost> BlogPosts_dbs { get; set; }
        public DbSet<BlogPostPostauthor> BlogPostPostauthors_dbs { get; set; }
        public DbSet<Category> Categories_dbs { get; set; }
        public DbSet<City> Cities_dbs { get; set; }
        public DbSet<Comment> Comments_dbs { get; set; }
        public DbSet<CommentAuthor> CommentAuthors_dbs { get; set; }
        public DbSet<PostAuthor> PostAuthors_dbs { get; set; }
        public DbSet<PostCategory> PostCategories_dbs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // first join table
            modelBuilder.Entity<PostCategory>()
                .HasKey(pa => new { pa.BlogPostId, pa.CategoryId });
            // meaning of this expression: I haveOne here, WithMany from there, with this HasForeignKey
            modelBuilder.Entity<PostCategory>()
                .HasOne(p => p.BlogPostJT)
                .WithMany(po => po.PostCategories)
                .HasForeignKey(p => p.BlogPostId);
            modelBuilder.Entity<PostCategory>()
                .HasOne(p => p.CategoryJT)
                .WithMany(p => p.PostCategories)
                .HasForeignKey(p => p.CategoryId);

            // second join table
            modelBuilder.Entity<BlogPostPostauthor>()
                .HasKey(p => new { p.PostAuthorId, p.BlogPostId});
            modelBuilder.Entity<BlogPostPostauthor>()
                .HasOne(p => p.PostAuthorJT)
                .WithMany(p => p.BlogPostPostauthors)
                .HasForeignKey(p => p.PostAuthorId);
            modelBuilder.Entity<BlogPostPostauthor>()
                .HasOne(p => p.BlogPostJT)
                .WithMany(p => p.BlogPostPostauthors)
                .HasForeignKey(p => p.BlogPostId);

        }
    }
}
