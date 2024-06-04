using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<Difficulty> Difficulties => Set<Difficulty>();
        public DbSet<Region> Regions => Set<Region>();
        public DbSet<Walk> Walks => Set<Walk>();
    }
}
