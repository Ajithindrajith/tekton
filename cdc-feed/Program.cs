var service = new ChangeFeedService();
await service.StartAsync();

Console.WriteLine("Listening to Cosmos DB changes...");
await Task.Delay(Timeout.Infinite);