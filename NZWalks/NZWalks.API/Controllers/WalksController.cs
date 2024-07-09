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
}
