using Microsoft.EntityFrameworkCore;

namespace UrlShorteningService.Models
{
    public class UrlContext : DbContext
    {
        public DbSet<UrlEntity> UrlEntities { get; set; }
        public UrlContext(DbContextOptions<UrlContext> options) : base(options)
        {
        }

    }
}
