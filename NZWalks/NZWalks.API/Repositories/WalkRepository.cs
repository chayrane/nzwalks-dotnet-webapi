using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories;

public class WalkRepository : IWalkRepository
{
    private readonly NZWalksDbContext _dbContext;

    public WalkRepository(NZWalksDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Walk> CreateAsync(Walk walk)
    {
        await _dbContext.Walks.AddAsync(walk);
        await _dbContext.SaveChangesAsync();

        return walk;
    }
}
