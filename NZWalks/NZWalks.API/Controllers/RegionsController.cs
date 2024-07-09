using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
    private readonly IMapper _mapper;

    public RegionsController(
        IRegionRepository regionRepository,
        IMapper mapper)
    {
        _regionRepository = regionRepository;
        _mapper = mapper;
    }

    // Get All Regions
    // GET: https://localhost:7258/api/regions
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        // Get Data from Database - using Domain Model.
        var regions = await _regionRepository.GetAllAsync();

        // Map Domain Model to DTO.
        var regionsDto = _mapper.Map<List<RegionDto>>(regions);

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

        // Map Domain Model to DTO
        var regionDto = _mapper.Map<RegionDto>(region);

        return Ok(regionDto);
    }

    // POST to create new region.
    // POST: https://localhost:7258/api/regions
    [HttpPost]
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
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromBody] UpdateRegionRequestDto updateRegion)
    {
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
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var regionDomainModel = await _regionRepository.DeleteAsync(id);

        // Map the Domain Model to DTO.
        var regionDto = _mapper.Map<RegionDto>(regionDomainModel);

        return Ok(regionDto);
    }
}
