using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using System.Text;

public class EventHubService
{
    private readonly string connectionString;
    private readonly string eventHubName = "pet-events";

    public EventHubService()
    {
        connectionString = File.ReadAllText("/mnt/secrets/EVENTHUBKEY").Trim();
    }

    public async Task SendAsync(string message)
    {
        await using var producer = new EventHubProducerClient(connectionString, eventHubName);

        using EventDataBatch batch = await producer.CreateBatchAsync();

        batch.TryAdd(new EventData(Encoding.UTF8.GetBytes(message)));

        await producer.SendAsync(batch);
    }
}