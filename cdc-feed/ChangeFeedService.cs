using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;

public class ChangeFeedService
{
    private readonly string databaseName = "petdb";
    private readonly string containerName = "pets";
    private readonly string leaseContainerName = "leases";

    private readonly EventHubService eventHub = new EventHubService();

    public async Task StartAsync()
    {
        var key = File.ReadAllText("/mnt/secrets/COSMOSKEY").Trim();
        var endpoint = "https://antdemonew.documents.azure.com:443/";

        CosmosClient client = new CosmosClient(endpoint, key);

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

            try
            {
                Console.WriteLine("📤 Sending to Event Hub...");

                string json = JsonConvert.SerializeObject(item);

                await eventHub.SendAsync(json);

                Console.WriteLine("✅ Sent to Event Hub");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
        }
    }
}