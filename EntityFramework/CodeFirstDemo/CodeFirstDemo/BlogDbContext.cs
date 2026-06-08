using System.Data.Entity;

namespace CodeFirstDemo
{
    public class BlogDbContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
    }
}
