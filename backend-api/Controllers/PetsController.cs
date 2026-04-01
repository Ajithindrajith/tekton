using Microsoft.AspNetCore.Mvc;
using PetApi.Models;
using PetApi.Services;

namespace PetApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : ControllerBase
    {
        private readonly CosmosDbService _cosmosService;

        public PetsController(CosmosDbService cosmosService)
        {
            _cosmosService = cosmosService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pet>>> Get()
        {
            var pets = await _cosmosService.GetPetsAsync();
            return Ok(pets);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pet>> GetById(string id)
        {
            var pet = await _cosmosService.GetPetByIdAsync(id);
            if (pet == null)
                return NotFound();
            return Ok(pet);
        }

        [HttpPost]
        public async Task<ActionResult<Pet>> Create([FromBody] Pet pet)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdPet = await _cosmosService.AddPetAsync(pet);
            return CreatedAtAction(nameof(GetById), new { id = createdPet.id }, createdPet);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Pet>> Update(string id, [FromBody] Pet pet)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedPet = await _cosmosService.UpdatePetAsync(id, pet);
            return Ok(updatedPet);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _cosmosService.DeletePetAsync(id);
            return NoContent();
        }
    }
}