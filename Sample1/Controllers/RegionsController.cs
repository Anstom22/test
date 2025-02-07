using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sample1.Data;
using Sample1.Models.Domain;
using Sample1.Models.DTO;
using Sample1.Repositories;

namespace Sample1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly AppDbContext dbContext;
        private readonly IRegionRepository regionRepository;

        public RegionsController(AppDbContext dbContext,IRegionRepository regionRepository)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regions = await regionRepository.GetAllAsync();
            var regionDto = new List<RegionDTO>();
            foreach (var region in regions)
            {
                regionDto.Add(new RegionDTO()
                {
                    Id = region.Id,
                    Name = region.Name,
                    Code = region.Code

                });

            }
            return Ok(regionDto);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var region = await regionRepository.GetByIdAsync(id);
            //var rg = dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if (region == null)
            {
            
                return NotFound();
            }
            var regionDto = new RegionDTO
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code
            };
            return Ok(regionDto);

        }
        [HttpPost]
        public async Task<IActionResult> Create(AddRegionDTO addRegionDTO)
        {
            var region = new Region()
            {
                Name = addRegionDTO.Name,
                Code = addRegionDTO.Code
            };
            region=await regionRepository.CreateAsync(region);
            

            var regionDto = new RegionDTO
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code
            };
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRegion(int id, UpdateRegionDTO updateRegionDTO)
        {
            var regions = new Region
            {
                Code = updateRegionDTO.Code,
                Name = updateRegionDTO.Name
            };
            regions = await regionRepository.UpdateRegionAsync(id, regions );
            if (regions == null)
            {
                return NotFound();
            }

            var regionDto = new RegionDTO
            {
                Id = regions.Id,
                Name = regions.Name,
                Code = regions.Code
            };
            return Ok(regionDto);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegion(int id)
        {
            var region = await regionRepository.DeleteRegionAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            
            var regionDto = new RegionDTO
            {

                Id = region.Id, 
                Name = region.Name,
                Code = region.Code

            };
            return Ok(regionDto);
        }
    }
}
