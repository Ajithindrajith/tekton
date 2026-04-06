using System.Net;
using Microsoft.Azure.Cosmos;

public class ChangeFeedService
{
    private readonly string databaseName = "PetDB";
    private readonly string containerName = "pets";
    private readonly string leaseContainerName = "leases";

    public async Task StartAsync()
    {
        // 🔐 Read from mounted file (CSI)
        var key = (await File.ReadAllTextAsync("/mnt/secrets/COSMOSKEY")).Trim();

        var endpoint  = "https://democosmosant.documents.azure.com:443/";

        CosmosClient client = new CosmosClient(endpoint, key);

        var database = client.GetDatabase("petdb");
        var container = database.GetContainer("pets");
        var leaseContainer = database.GetContainer("leases");

        var processor = container
            .GetChangeFeedProcessorBuilder<dynamic>("pet-processor", HandleChangesAsync)
            .WithInstanceName(Environment.MachineName)
            .WithLeaseContainer(leaseContainer)
            .Build();

        await processor.StartAsync();

        Console.WriteLine("Change Feed Processor started...");
    }

    private async Task HandleChangesAsync(
        IReadOnlyCollection<dynamic> changes,
        CancellationToken cancellationToken)
    {
        foreach (var item in changes)
        {
            Console.WriteLine($"Change detected: {item}");

            // 👉 future: send to Event Hub
        }
    }
}