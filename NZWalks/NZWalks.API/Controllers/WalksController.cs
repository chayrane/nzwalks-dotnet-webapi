using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers;

// /api/walks
[Route("api/[controller]")]
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
    public async Task<IActionResult> Create([FromBody] AddWalkRequestDto walkRequestDto)
    {
        // Map AddWalkDto to Domain model.
        var walkDomainModel = _mapper.Map<Walk>(walkRequestDto);

        walkDomainModel = await _walkRepository.CreateAsync(walkDomainModel);

        // Map Domain model to DTO.
        var walkDto = _mapper.Map<WalkDto>(walkDomainModel);

        return Ok(walkDto);
    }

    // Get Walks
    // GET: /api/walks
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var walksDomainModel = await _walkRepository.GetAllAsync();

        // Map Domain to DTO
        var walkDto = _mapper.Map<List<WalkDto>>(walksDomainModel);

        return Ok(walkDto);
    }

    // Get Walk by Id
    // GET: /api/walks/:id
    [HttpGet]
    [Route("{id:Guid}")]
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
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
    {
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
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var walkDomainModel = await _walkRepository.DeleteAsync(id);

        if (walkDomainModel is null) return NotFound();

        // Map Domain Model to DTO.
        var WalkDto = _mapper.Map<WalkDto>(walkDomainModel);

        return Ok(WalkDto);
    }
}
