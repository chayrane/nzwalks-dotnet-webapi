using Microsoft.EntityFrameworkCore;
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

    public async Task<Walk?> DeleteAsync(Guid id)
    {
        var existingWalk = 
            await _dbContext.Walks
                    .FirstOrDefaultAsync(x => x.Id == id);

        if (existingWalk is null) return null;

        _dbContext.Walks.Remove(existingWalk);
        await _dbContext.SaveChangesAsync();

        return existingWalk;
    }

    public async Task<List<Walk>> GetAllAsync()
    {
        return await _dbContext.Walks
            .Include("Region")
            .Include("Difficulty")
            .ToListAsync();
    }

    public async Task<Walk?> GetByIdAsync(Guid id)
    {
        var walk = await _dbContext.Walks
            .Include("Region")
            .Include("Difficulty")
            .FirstOrDefaultAsync(x => x.Id == id);

        if (walk is null) return null;

        return walk;
    }

    public async Task<Walk> UpdateAsync(Guid id, Walk walk)
    {
        var existingWalk = 
            await _dbContext.Walks
                    .FirstOrDefaultAsync(x => x.Id == id);

        if (existingWalk is null) return null;

        existingWalk.Name = walk.Name;
        existingWalk.Description = walk.Description;
        existingWalk.LengthInKm = walk.LengthInKm;
        existingWalk.WalkImageUrl = walk.WalkImageUrl;
        existingWalk.RegionId = walk.RegionId;
        existingWalk.DifficultyId = walk.DifficultyId;

        await _dbContext.SaveChangesAsync();

        return existingWalk;
    }
}
