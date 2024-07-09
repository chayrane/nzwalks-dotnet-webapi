using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers;

// HTTP Endpoint: https://localhost:7258/api/regions
[Route("api/[controller]")]
[ApiController]
public class RegionsController : ControllerBase
{
    private readonly IRegionRepository _regionRepository;

    public RegionsController(IRegionRepository regionRepository)
    {
        _regionRepository = regionRepository;
    }

    // Get All Regions
    // GET: https://localhost:7258/api/regions
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        // Get Data from Database - using Domain Model.
        var regions = await _regionRepository.GetAllAsync();

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
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var region = await _regionRepository.GetByIdAsync(id);

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
    public async Task<IActionResult> Create([FromBody] AddRegionRequestDto region)
    {
        // Map the DTO to Domain model.
        var regionDomainModel = new Region()
        {
            Code = region.Code,
            Name = region.Name,
            RegionImageUrl = region.RegionImageUrl
        };

        // Use Domain model to create region.
        regionDomainModel = await _regionRepository.CreateAsync(regionDomainModel);

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
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegion)
    {
        // Map DTO to Domain model.
        var regionDomainModel = new Region()
        {
            Code = updateRegion.Code,
            Name = updateRegion.Name,
            RegionImageUrl = updateRegion.RegionImageUrl
        };

        regionDomainModel = await _regionRepository.UpdateAsync(id, regionDomainModel);

        if (regionDomainModel is null) return NotFound();

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
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        // Check if the region exist.
        var regionDomainModel = await _regionRepository.DeleteAsync(id);

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
