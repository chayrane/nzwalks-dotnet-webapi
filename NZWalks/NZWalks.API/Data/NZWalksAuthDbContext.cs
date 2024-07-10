using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.API.Data;

public class NZWalksAuthDbContext : IdentityDbContext
{
    public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        var readerRoleId = "f23ee1b3-92fd-4d75-9412-9ae942d88320";
        var writerRoleId = "49e85c16-aa38-443b-9d69-4b860f219be9";

        var roles = new List<IdentityRole>
        {
            // Reader Role.
            new IdentityRole
            {
                Id = readerRoleId,
                Name = "Reader",
                NormalizedName = "Reader".ToUpper(),
                ConcurrencyStamp = readerRoleId
            },

            // Writer Role.
            new IdentityRole
            {
                Id = writerRoleId,
                Name = "Writer",
                NormalizedName = "Writer".ToUpper(),
                ConcurrencyStamp = writerRoleId
            }
        };
        
        // Seeding data into database.
        builder.Entity<IdentityRole>().HasData(roles);
    }
}
