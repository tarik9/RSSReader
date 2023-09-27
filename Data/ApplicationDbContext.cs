using Microsoft.EntityFrameworkCore;
using MinimalRSSReader.Models; // Import your data models namespace

namespace MinimalRSSReader.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<RSSFeed> RSSFeeds { get; set; } // Replace RSSFeed with your data model
    }
}
