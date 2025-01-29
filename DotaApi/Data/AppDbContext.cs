using DotaApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotaApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
        }

        public DbSet<Hero> Heroes { get; set; }
        public DbSet<Item> Items { get; set; }
    }
}
