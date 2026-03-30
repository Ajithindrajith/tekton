using Microsoft.Azure.Cosmos;
using PetApi.Models;

namespace PetApi.Services
{
    public class CosmosDbService
    {
        private readonly Container _container;

        public CosmosDbService(IConfiguration configuration)
        {
            var endpoint = configuration["Cosmos:Endpoint"];
            var key = configuration["Cosmos:Key"];
            var database = configuration["Cosmos:Database"];
            var containerName = configuration["Cosmos:Container"];

            var client = new CosmosClient(endpoint, key);
            _container = client.GetContainer(database, containerName);
        }

        public async Task<List<Pet>> GetPetsAsync()
        {
            var query = "SELECT * FROM c";
            var iterator = _container.GetItemQueryIterator<Pet>(query);

            List<Pet> pets = new List<Pet>();

            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();
                pets.AddRange(response.ToList());
            }

            return pets;
        }
    }
}