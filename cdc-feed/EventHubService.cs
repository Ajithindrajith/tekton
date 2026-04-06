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

        var eventData = new EventData(Encoding.UTF8.GetBytes(message));

        if (!batch.TryAdd(eventData))
        {
            Console.WriteLine("❌ Event too large for batch!");
            return;
        }

        Console.WriteLine("✅ Event added to batch");

        await producer.SendAsync(batch);

        Console.WriteLine("🚀 Event sent to Event Hub");
    }
}