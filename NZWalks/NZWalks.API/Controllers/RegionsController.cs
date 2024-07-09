using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;

namespace NZWalks.API.Controllers;

// HTTP Endpoint: https://localhost:7258/api/regions
[Route("api/[controller]")]
[ApiController]
public class RegionsController : ControllerBase
{
    public readonly NZWalksDbContext _dbContext;

    public RegionsController(NZWalksDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Get All Regions
    // GET: https://localhost:7258/api/regions
    [HttpGet]
    public IActionResult GetAll()
    {
        var regions = _dbContext.Regions.ToList();

        return Ok(regions);
    }

    // Get Region by Id.
    // GET: https://localhost:7258/api/regions/:id
    [HttpGet]
    [Route("{id}")]
    public IActionResult Get([FromRoute] Guid id)
    {
        var region = _dbContext.Regions.Find(id);

        // var region = _dbContext.Regions.FirstOrDefault(x => x.Id == id);

        if (region is null) return NotFound();

        return Ok(region);
    }

}
