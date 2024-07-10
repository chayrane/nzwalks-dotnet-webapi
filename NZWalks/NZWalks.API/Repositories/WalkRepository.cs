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

    public async Task<List<Walk>> GetAllAsync(
        string? filterOn = null,
        string? filterQuery = null,
        string? sortBy = null,
        bool isAscending = true,
        int pageNumber = 1,
        int pageSize = 100)
    {
        var walks = _dbContext.Walks
            .Include("Region")
            .Include("Difficulty")
            .AsQueryable();

        // Filtering
        if (!string.IsNullOrWhiteSpace(filterOn) &&
            !string.IsNullOrWhiteSpace(filterQuery))
        {
            // Filtering Name Column.
            if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
            {
                walks = walks.Where(x => x.Name.Contains(filterQuery));
            }
        }

        // Sorting
        if (!string.IsNullOrWhiteSpace(sortBy))
        {
            // Sorting on Name Column.
            if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
            {
                walks = isAscending ? walks.OrderBy(x => x.Name) :
                    walks.OrderByDescending(x => x.Name);
            }

            // Sorting on Length in KM Column.
            if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
            {
                walks = isAscending ? walks.OrderBy(x => x.LengthInKm) :
                    walks.OrderByDescending(x => x.LengthInKm);
            }
        }

        // Pagination
        var skipResults = (pageNumber - 1) * pageSize;

        return await walks.Skip(skipResults).Take(pageSize).ToListAsync();

        //return await _dbContext.Walks
        //    .Include("Region")
        //    .Include("Difficulty")
        //    .ToListAsync();
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
