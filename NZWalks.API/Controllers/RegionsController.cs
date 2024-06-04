using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository _regionRepository;

        public RegionsController(
            IRegionRepository regionRepository
            )
        {
            _regionRepository = regionRepository;
        }

        // GET ALL REGIONS
        // GET: https://localhost:7010/api/Regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Get Data From Database - Domains Model
            var regionsDomain = await _regionRepository.GetAllAsync();

            // Map Domains Model To DTOs 
            var regionDto = new List<RegionDto>();

            foreach (var regionDomain in regionsDomain)
            {
                regionDto.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Name = regionDomain.Name,
                    Code = regionDomain.Code,
                    RegionImageUrl = regionDomain.RegionImageUrl,
                });
            }
            // Return DTOs
            return Ok(regionDto);
        }

        // GET ALL REGION BY ID
        // GET: https://localhost:7010/api/Regions/get-by-id/yourId 
        [HttpGet("get-by-id/{id:guid}")]
        public async Task<IActionResult> GetRegionById(Guid id) 
        {
            var regionsDomain = await _regionRepository.GetByIdAsync(id);

            if (regionsDomain == null)
            {
                return NotFound();
            }

            var regionDto = new RegionDto
            {
                Id = regionsDomain.Id,
                Name = regionsDomain.Name,
                Code = regionsDomain.Code,
                RegionImageUrl = regionsDomain.RegionImageUrl,
            };

            return Ok(regionDto);
        }

        // CREATE REGION
        // POST: https://localhost:7010/api/Regions
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateRegionDto createRegionDto)
        {
            // Map or Convert DTO to Domain Model
            var regionDomainModel = new Region
            {
                Name = createRegionDto.Name,
                Code = createRegionDto.Code,
                RegionImageUrl = createRegionDto.RegionImageUrl,
            };

            // Use Domain Model to create Region
            await _regionRepository.CreatedAsync(regionDomainModel);

            // Map Domain Model back to DTO
            var regionDto = new CreateRegionDto
            {
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };

            return CreatedAtAction(nameof(GetRegionById), new {id = regionDomainModel.Id}, regionDto);
        }

        // UPDATE REGION
        // PUT: https://localhost:7010/api/Regions/update/YOUR_ID
        [HttpPut("update/{id:guid}")]
        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody]UpdateRegionDto updateRegion) 
        {
            // Map DTO to Model
            var regionDomain = new Region
            {
                Name = updateRegion.Name,
                Code = updateRegion.Code,
                RegionImageUrl = updateRegion.RegionImageUrl,
            };

            regionDomain = await _regionRepository.UpdatedAsync(id, regionDomain);

            if (regionDomain == null)
            {
                return NotFound();
            }

            // Convert Domain Model to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl,
            };

            return Ok(regionDto);
        }

        // DELETE REGION
        // PUT: https://localhost:7010/api/Regions/update/YOUR_ID
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            await _regionRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
