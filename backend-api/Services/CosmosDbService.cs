using Microsoft.Azure.Cosmos;
using PetApi.Models;

namespace PetApi.Services;

public class CosmosDbService
{
    private readonly Container _container;

    public CosmosDbService(IConfiguration configuration)
    {
        var client = new CosmosClient(
            configuration["Cosmos:Endpoint"],
            configuration["Cosmos:Key"]
        );

        var database = client.GetDatabase(configuration["Cosmos:Database"]);
        _container = database.GetContainer(configuration["Cosmos:Container"]);
    }

    public async Task AddPetAsync(Pet pet)
    {
        await _container.CreateItemAsync(pet, new PartitionKey(pet.Type));
    }

    public async Task<List<Pet>> GetPetsAsync()
    {
        var query = _container.GetItemQueryIterator<Pet>("SELECT * FROM c");
        List<Pet> results = new();

        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            results.AddRange(response);
        }

        return results;
    }
}