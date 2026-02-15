
using GYMIND.API.DTOs;
using GYMIND.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GYMIND.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GymController : ControllerBase
    {
        private readonly IGymService _gymService;

        public GymController(IGymService gymService)
        {
            _gymService = gymService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGyms()
        {
            var gyms = await _gymService.GetAllGymsAsync();
            return Ok(gyms);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetGym(Guid id)
        {
            var gym = await _gymService.GetGymByIdAsync(id);
            if (gym == null)
                return NotFound();

            return Ok(gym);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetGymByName(string name)
        {
            var gym = await _gymService.GetGymByNameAsync(name);
            if (gym == null)
                return NotFound();

            return Ok(gym);
        }

        [HttpGet("address/{address}")]
        public async Task<IActionResult> GetGymByAddress(string address)
        {
            var gyms = await _gymService.GetGymsByAddressAsync(address);
            if (!gyms.Any())
                return NotFound();

            return Ok(gyms);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGym ([FromBody] GymDto dto)
        {
            var createdGym = await _gymService.CreateGymAsync(dto);
            return CreatedAtAction(nameof(GetGym), new { id = createdGym.GymId }, createdGym);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateGym(Guid id, [FromBody] GymDto dto)
        {
            var success = await _gymService.UpdateGymAsync(id, dto);
            if (!success)
                return NotFound();

            return NoContent();
        }

        //ApproveGym

        // CreateBranchAsync
        [HttpPost("{gymId:guid}/branches")]
        public async Task<IActionResult> CreateBranch(Guid gymId, [FromBody] GymBranchDto dto)
        {
            var branch = await _gymService.CreateBranchAsync(gymId, dto);

            return CreatedAtAction(
                nameof(CreateBranch),
                new { gymId = gymId },
                branch
            );
        }
        
        // UpdateBranchAsync
        [HttpPut("branches/{branchId:guid}")]
        public async Task<IActionResult> UpdateBranch(Guid branchId, [FromBody] GymBranchDto dto)
        {
            var success = await _gymService.UpdateBranchAsync(branchId, dto);
            if (!success)
                return NotFound();

            return NoContent();
        }


    }
}
