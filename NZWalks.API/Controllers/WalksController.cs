using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository _walkRepository;
        private readonly IMapper _mapper;

        public WalksController(IWalkRepository walkRepository,
            IMapper mapper)
        {
            _walkRepository = walkRepository;
            _mapper = mapper;
        }

        // CREATE WALK
        // POST: https://localhost:7010/api/Walks
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateWalksDto createWalksDto)
        {
            var walk = _mapper.Map<Walk>(createWalksDto);

            await _walkRepository.Create(walk);

            var walksDto = _mapper.Map<WalkDto>(walk);

            return CreatedAtAction(nameof(GetById), new { id = walk.Id }, walksDto);
        }

        // GET ALL 
        // GET: https://localhost:7010/api/Walks
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walks = await _walkRepository.GetAllAsync();
            var walkDto = _mapper.Map<List<WalkDto>>(walks);
            return Ok(walkDto);
        }

        //GET BY ID
        // GET: https://localhost:7010/api/Walks/get-by-id/YOUR_ID
        [HttpGet("get-by-id/{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            var walk = await _walkRepository.GetByIdAsync(id);

            if(walk == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<WalkDto>(walk));    
        }

        // UPDATE
        // PUT: https://localhost:7010/api/Walks/update/YOUR_ID
        [HttpPut("update/{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkDto updateWalkDto)
        {
            var walkDomain = _mapper.Map<Walk>(updateWalkDto);
            walkDomain = await _walkRepository.UpdateAsync(id, walkDomain);

            if (walkDomain == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<WalkDto>(walkDomain));
        }
    }
}
