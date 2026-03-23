using Microsoft.AspNetCore.Mvc;
using PetApi.Models;
using PetApi.Services;

namespace PetApi.Controllers;

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
    public async Task<IActionResult> Get()
    {
        var pets = await _cosmosService.GetPetsAsync();
        return Ok(pets);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Pet pet)
    {
        await _cosmosService.AddPetAsync(pet);
        return Ok(pet);
    }
}