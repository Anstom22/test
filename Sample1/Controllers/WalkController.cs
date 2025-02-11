using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sample1.Models.Domain;
using Sample1.Models.DTO;
using Sample1.Repositories;
using System.Net.WebSockets;

namespace Sample1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalkController : ControllerBase
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalkController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Create(AddWalkDTO addWalkDTO)
        {
            if (ModelState.IsValid)
            {
                var walk = mapper.Map<Walk>(addWalkDTO);
                walk = await walkRepository.CreateAsync(walk);
                var walkDto = mapper.Map<WalkDTO>(walk);
                return CreatedAtAction(nameof(Create), new { id = walkDto.Id }, walkDto);
            }
            return BadRequest(ModelState);
            

        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walk = await walkRepository.GetAllAsync();
            var walkDto = mapper.Map<List<WalkDTO>>(walk);
            return Ok(walkDto);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetWalkById(int id)
        {
            var walk = await walkRepository.GetWalkByIdAsync(id);
            if (walk == null)
            {
                return NotFound();
            }
            var walkDto = mapper.Map<WalkDTO>(walk);
            return Ok(walkDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWalk(int id, UpdatewalkDTO updatewalkDTO)
        {
            if (ModelState.IsValid)
            {
                var walk = mapper.Map<Walk>(updatewalkDTO);
                walk = await walkRepository.UpdateWalkAsync(id, walk);
                if (walk == null)
                {
                    return NotFound();
                }
                var walkDto = mapper.Map<WalkDTO>(walk);
                return Ok(walkDto);

            }
            return BadRequest(ModelState);
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteWalk(int id)
        {
            var walk= await walkRepository.DeleteWalkAsync(id);
            if (walk == null)
            {
                return NotFound();
            }
            var walkDto= mapper.Map<WalkDTO>(walk);
            return Ok(walkDto);
        }
            
    }
}
