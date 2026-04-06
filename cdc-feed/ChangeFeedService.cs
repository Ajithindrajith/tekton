using Microsoft.Azure.Cosmos;

public class ChangeFeedService
{
    private readonly string databaseName = "PetDB";
    private readonly string containerName = "pets";
    private readonly string leaseContainerName = "leases";

    public async Task StartAsync()
    {
        // 🔐 Read from mounted file (CSI)
        var connectionString = (await File.ReadAllTextAsync("/mnt/secrets/COSMOSKEY")).Trim();

        Console.WriteLine($"Conn Length: {connectionString.Length}");

        CosmosClient client = new CosmosClient(connectionString);

        var database = client.GetDatabase(databaseName);
        var container = database.GetContainer(containerName);
        var leaseContainer = database.GetContainer(leaseContainerName);

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