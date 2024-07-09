namespace NZWalks.API.Models.Domain;

public class Walk
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? WalkImageUrl { get; set; }
    
    public Guid RegionId { get; set; }
    public Guid DifficultyId { get; set; }

    // Navigation Property - Defining relationships with tables.
    public Region Region { get; set; }
    public Difficulty Difficulty { get; set; }
}
