using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data;

public class NZWalksDbContext : DbContext
{
    public NZWalksDbContext(DbContextOptions options) : base(options)
    {
    }

    // DbSets - Property of DbContext class that represents
    //          a collections of entities in the database.
    public DbSet<Walk> Walks { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Difficulty> Difficulty { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed data for Difficulties.
        // Easy, Medium, Hard
        var difficulties = new List<Difficulty>()
        {
            new Difficulty()
            {
                Id = Guid.Parse("a099141b-063f-4a33-ac06-d7fd87f83adf"),
                Name = "Easy"
            },
            new Difficulty()
            {
                Id = Guid.Parse("6dbd7c6c-950d-4fe8-89bf-bcdb1528e388"),
                Name = "Medium"
            },new Difficulty()
            {
                Id = Guid.Parse("a6c2969c-ed99-49bc-ae68-d8bc2395a09a"),
                Name = "Hard"
            }
        };

        // Seed Difficulties to the Database.
        modelBuilder.Entity<Difficulty>()
            .HasData(difficulties);

        // Seed Data for Regions
        var regions = new List<Region>()
        {
            new Region()
            {
                Id = Guid.Parse("377ae495-f8c1-47d8-9c52-cc1127f13f2b"),
                Code = "AKL",
                Name = "Auckland",
                RegionImageUrl = "https://images.pexels.com/photos/831910/pexels-photo-831910.jpeg"
            },
            new Region()
            {
                Id = Guid.Parse("a8c61f5c-b1be-4c17-b8fa-b8ea1f03dd83"),
                Code = "NTL",
                Name = "Northland",
                RegionImageUrl = "https://images.pexels.com/photos/1022479/pexels-photo-1022479.jpeg"
            },new Region()
            {
                Id = Guid.Parse("57e08216-a177-4cb7-a15c-aa867b2d3f9f"),
                Code = "BOP",
                Name = "Bay of Plenty",
                RegionImageUrl = "https://images.pexels.com/photos/403781/pexels-photo-403781.jpeg"
            },
            new Region
            {
                Id = Guid.Parse("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"),
                Code = "WGN",
                Name = "Wellington",
                RegionImageUrl = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg"
            },
            new Region
            {
                Id = Guid.Parse("906cb139-415a-4bbb-a174-1a1faf9fb1f6"),
                Code = "NSN",
                Name = "Nelson",
                RegionImageUrl = "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg"
            },
            new Region
            {
                Id = Guid.Parse("f077a22e-4248-4bf6-b564-c7cf4e250263"),
                Code = "STL",
                Name = "Southland",
                RegionImageUrl = "https://images.pexels.com/photos/3396655/pexels-photo-3396655.jpeg"
            }
        };

        // Seed Regions to the Database.
        modelBuilder.Entity<Region>()
            .HasData(regions);
    }
}
