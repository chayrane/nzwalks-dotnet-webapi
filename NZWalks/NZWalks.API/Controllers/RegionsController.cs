using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Repositories;
using System.Text.Json;

namespace NZWalks.API.Controllers;

// HTTP Endpoint: https://localhost:7258/api/regions
[Route("api/regions")]
[ApiController]
public class RegionsController : ControllerBase
{
    private readonly IRegionRepository _regionRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<RegionsController> _logger;

    public RegionsController(
        IRegionRepository regionRepository,
        IMapper mapper,
        ILogger<RegionsController> logger)
    {
        _regionRepository = regionRepository;
        _mapper = mapper;
        _logger = logger;
    }

    // Get All Regions
    // GET: https://localhost:7258/api/regions
    [HttpGet]
    [Authorize(Roles = "Reader,Writer")]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("GetAll Action Method Invoked");

        // Get Data from Database - using Domain Model.
        var regions = await _regionRepository.GetAllAsync();

        // Map Domain Model to DTO.
        var regionsDto = _mapper.Map<List<RegionDto>>(regions);

        _logger.LogInformation($"Finished GetAllRegions" +
            $" request with data: {JsonSerializer.Serialize(regionsDto)}");

        // Return DTO to client.
        return Ok(regionsDto);
    }

    // Get Region by Id.
    // GET: https://localhost:7258/api/regions/:id
    [HttpGet]
    [Route("{id:Guid}")]
    [Authorize(Roles = "Reader,Writer")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var region = await _regionRepository.GetByIdAsync(id);

        if (region is null) return NotFound();

        // Map Domain Model to DTO
        var regionDto = _mapper.Map<RegionDto>(region);

        return Ok(regionDto);
    }

    // POST to create new region.
    // POST: https://localhost:7258/api/regions
    [HttpPost]
    [ValidateModel]
    [Authorize(Roles = "Writer")]
    public async Task<IActionResult> Create([FromBody] AddRegionRequestDto region)
    {
        // Map RequestDto to Domain Model
        var regionDomainModel = _mapper.Map<Region>(region);

        // Use Domain model to create Region.
        regionDomainModel = 
            await _regionRepository.CreateAsync(regionDomainModel);

        // Map Domain model to DTO
        var regionDto = _mapper.Map<RegionDto>(regionDomainModel);

        // Returns HTTP 201 Status
        return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
    }

    // PUT to update a region
    // PUT: https://localhost:7258/api/regions/:id
    [HttpPut]
    [Route("{id:Guid}")]
    [Authorize(Roles = "Writer")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromBody] UpdateRegionRequestDto updateRegion)
    {
        // Validate Request Model manually without ValidateModel attribute.
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Map RequestDto to Domain model.
        var regionDomainModel = _mapper.Map<Region>(updateRegion);

        regionDomainModel = 
            await _regionRepository.UpdateAsync(id, regionDomainModel);

        if (regionDomainModel is null) return NotFound();

        // Map the Domain Model to DTO.
        var regionDto = _mapper.Map<RegionDto>(regionDomainModel);

        return Ok(regionDto);
    }

    // Delete a region.
    // DELETE: https://localhost:7258/api/regions/:id
    [HttpDelete]
    [Route("{id:Guid}")]
    [Authorize(Roles = "Writer")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var regionDomainModel = await _regionRepository.DeleteAsync(id);

        // Map the Domain Model to DTO.
        var regionDto = _mapper.Map<RegionDto>(regionDomainModel);

        return Ok(regionDto);
    }
}
