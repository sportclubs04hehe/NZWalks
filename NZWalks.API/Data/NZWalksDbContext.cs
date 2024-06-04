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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Data for Dificulties
            // Easy, Medium, Hard

            var difficulties = new List<Difficulty>
            {
                new Difficulty
                {
                    Id = Guid.Parse("c39c996d-8e8d-497a-ae47-dd47de71c91d"),
                    Name = "Easy"
                },
                 new Difficulty
                {
                    Id = Guid.Parse("621a47c3-1802-4035-a554-f0384ef752bb"),
                    Name = "Medium"
                },
                  new Difficulty
                {
                    Id = Guid.Parse("1e224fad-5f91-412b-8856-9ec6e6c8f742"),
                    Name = "Hard"
                },
            };
            // Seed difficulties to the database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            // Seed Data for Region
            var regions = new List<Region>
            {
                new Region
                {
                    Id = Guid.Parse("adf7e9d5-5c69-46a7-ab72-b63a0422bf3f"),
                    Name = "RONALDO",
                    Code = "SIUU",
                    RegionImageUrl = "https://images.pexels.com/photos/3225517/pexels-photo-3225517.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
                },
                 new Region
                {
                    Id = Guid.Parse("1a01cb28-325b-44d5-a428-bc642120ae31"),
                    Name = "MESSI",
                    Code = "XIUUU",
                    RegionImageUrl = "https://images.pexels.com/photos/2592884/pexels-photo-2592884.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
                },
                  new Region
                {
                    Id = Guid.Parse("57d2b2f6-8e1a-4ea1-9158-874be2779ac6"),
                    Name = "ANH NAM",
                    Code = "KEKE",
                    RegionImageUrl = "https://images.pexels.com/photos/2582614/pexels-photo-2582614.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
                },
                   new Region
                {
                    Id = Guid.Parse("ff70aff2-a853-468c-b612-0015762ca4b6"),
                    Name = "ABC",
                    Code = "XYZ",
                    RegionImageUrl = "https://images.pexels.com/photos/3225517/pexels-photo-3225517.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
                },
                    new Region
                {
                    Id = Guid.Parse("22e0bb74-dd7b-4050-afd1-a792ff68cba2"),
                    Name = "HUHI",
                    Code = "BLA BLA",
                    RegionImageUrl = "https://images.pexels.com/photos/3225517/pexels-photo-3225517.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
                },
            };
            // Seed difficulties to the database
            modelBuilder.Entity<Region>().HasData(regions);


        }
    }
}
