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
}
