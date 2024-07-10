using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers;

// /api/walks
[Route("api/walks")]
[ApiController]
public class WalksController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IWalkRepository _walkRepository;

    public WalksController(
        IMapper mapper,
        IWalkRepository walkRepository)
    {
        _mapper = mapper;
        _walkRepository = walkRepository;
    }

    // CREATE Walk
    // POST: /api/walks
    [HttpPost]
    [ValidateModel]
    [Authorize(Roles = "Writer")]
    public async Task<IActionResult> Create([FromBody] AddWalkRequestDto walkRequestDto)
    {
        // Validate Request Model.
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Map AddWalkDto to Domain model.
        var walkDomainModel = _mapper.Map<Walk>(walkRequestDto);

        walkDomainModel = await _walkRepository.CreateAsync(walkDomainModel);

        // Map Domain model to DTO.
        var walkDto = _mapper.Map<WalkDto>(walkDomainModel);

        return Ok(walkDto);
    }

    // Get Walks
    // GET: /api/walks
    // GET: /api/walks?filterOn=Name&filterQuery=Track
    // GET: /api/walks?filterOn=Name&filterQuery=Track&sortBy=Name&isAscending=true
    // GET: /api/walks?filterOn=Name&filterQuery=Track&sortBy=Name&isAscending=true&pageNumber=1&pageSize=10
    [HttpGet]
    [Authorize(Roles = "Reader,Writer")]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? filterOn,
        [FromQuery] string? filterQuery,
        [FromQuery] string? sortBy,
        [FromQuery] bool? isAscending,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 100)
    {
        var walksDomainModel = await _walkRepository
            .GetAllAsync(
                filterOn, filterQuery,
                sortBy, isAscending ?? true,
                pageNumber, pageSize);

        // Map Domain to DTO
        var walkDto = _mapper.Map<List<WalkDto>>(walksDomainModel);

        return Ok(walkDto);
    }

    // Get Walk by Id
    // GET: /api/walks/:id
    [HttpGet]
    [Route("{id:Guid}")]
    [Authorize(Roles = "Reader,Writer")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var walkDomainModel = await _walkRepository.GetByIdAsync(id);

        if (walkDomainModel is null) return NotFound();

        // Map Domain to DTO.
        var walkDto = _mapper.Map<WalkDto>(walkDomainModel);

        return Ok(walkDto);
    }

    // Update Walk by id
    // PUT: /api/walks/:id
    [HttpPut]
    [Route("{id:Guid}")]
    [Authorize(Roles = "Writer")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
    {
        // Validate Request Model manually without ValidateModel attribute.
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Map the updateWalkRequestDto to Domain model.
        var walkDomainModel = _mapper.Map<Walk>(updateWalkRequestDto);

        walkDomainModel = 
            await _walkRepository.UpdateAsync(id, walkDomainModel);

        if (walkDomainModel is null) return NotFound();

        // Map the Domain Model to DTO
        var walkDto = _mapper.Map<WalkDto>(walkDomainModel);

        return Ok(walkDto);
    }

    // Delete a Walk by Id
    // DELETE: /api/walks/:id
    [HttpDelete]
    [Route("{id:Guid}")]
    [Authorize(Roles = "Writer")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var walkDomainModel = await _walkRepository.DeleteAsync(id);

        if (walkDomainModel is null) return NotFound();

        // Map Domain Model to DTO.
        var WalkDto = _mapper.Map<WalkDto>(walkDomainModel);

        return Ok(WalkDto);
    }
}
