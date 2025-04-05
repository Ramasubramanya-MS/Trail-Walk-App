using Microsoft.EntityFrameworkCore;
using NatureWalks.API.Models.Domain;

namespace NatureWalks.API.Data
{
    public class WalksDbContext:DbContext
    {
        public WalksDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {
            
        }

        // Represents collections and creates tables inside Database
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

    }
}
