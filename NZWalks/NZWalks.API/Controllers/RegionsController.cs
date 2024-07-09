using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;

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
        // Get Data from Database - using Domain Model.
        var regions = _dbContext.Regions.ToList();

        // Map Domain Model to DTO.
        var regionsDto = new List<RegionDto>();

        foreach(var region in regions)
        {
            regionsDto.Add(new RegionDto()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            });
        }

        // Return DTO to client.
        return Ok(regionsDto);
    }

    // Get Region by Id.
    // GET: https://localhost:7258/api/regions/:id
    [HttpGet]
    [Route("{id:Guid}")]
    public IActionResult GetById([FromRoute] Guid id)
    {
        var region = _dbContext.Regions.Find(id);

        // var region = _dbContext.Regions.FirstOrDefault(x => x.Id == id);

        if (region is null) return NotFound();

        var regionDto = new RegionDto()
        {
            Id = region.Id,
            Code = region.Code,
            Name = region.Name,
            RegionImageUrl = region.RegionImageUrl
        };

        return Ok(regionDto);
    }

    // POST to create new region.
    // POST: https://localhost:7258/api/regions
    [HttpPost]
    public IActionResult Create([FromBody] AddRegionRequestDto region)
    {
        // Map the DTO to Domain model.
        var regionDomainModel = new Region()
        {
            Code = region.Code,
            Name = region.Name,
            RegionImageUrl = region.RegionImageUrl
        };

        // Use Domain model to create region.
        _dbContext.Regions.Add(regionDomainModel);
        _dbContext.SaveChanges();

        // Map Domain model to DTO
        var regionDto = new RegionDto()
        {
            Id = regionDomainModel.Id,
            Code = regionDomainModel.Code,
            Name = regionDomainModel.Name,
            RegionImageUrl = regionDomainModel.RegionImageUrl
        };

        // Returns HTTP 201 Status
        return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
    }

    // PUT to update a region
    // PUT: https://localhost:7258/api/regions/:id
    [HttpPut]
    [Route("{id:Guid}")]
    public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegion)
    {
        // Check if Region exists.
        var regionDomainModel = _dbContext.Regions.FirstOrDefault(x => x.Id == id);

        if (regionDomainModel is null) return NotFound();

        // Map DTO to Domain model
        regionDomainModel.Code = updateRegion.Code;
        regionDomainModel.Name = updateRegion.Name;
        regionDomainModel.RegionImageUrl = updateRegion.RegionImageUrl;

        _dbContext.SaveChanges();

        // Convert Domain model to DTO
        var regionDto = new RegionDto()
        {
            Id = regionDomainModel.Id,
            Code = regionDomainModel.Code,
            Name = regionDomainModel.Name,
            RegionImageUrl = regionDomainModel.RegionImageUrl
        };

        return Ok(regionDto);
    }

    // Delete a region.
    // DELETE: https://localhost:7258/api/regions/:id
    [HttpDelete]
    [Route("{id:Guid}")]
    public IActionResult Delete([FromRoute] Guid id)
    {
        // Check if the region exist.
        var regionDomainModel = _dbContext.Regions.FirstOrDefault(x => x.Id == id);

        if (regionDomainModel is null) return NotFound();

        // Delete Region.
        _dbContext.Regions.Remove(regionDomainModel);
        _dbContext.SaveChanges();

        // Return Deleted Region back to view.
        // Map the Domain Model to DTO.
        var regionDto = new RegionDto()
        {
            Id = regionDomainModel.Id,
            Code = regionDomainModel.Code,
            Name = regionDomainModel.Name,
            RegionImageUrl = regionDomainModel.RegionImageUrl
        };

        return Ok(regionDto);
    }
}
