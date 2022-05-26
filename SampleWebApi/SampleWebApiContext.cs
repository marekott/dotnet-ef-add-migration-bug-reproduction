using Microsoft.EntityFrameworkCore;

namespace SampleWebApi
{
    public class SampleWebApiContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }

        public SampleWebApiContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("SampleWebApi");
            base.OnModelCreating(modelBuilder);
        }
    }

    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }
        public int Rating { get; set; }
    }
}
