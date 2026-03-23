namespace PetApi.Models;

public class Pet
{
    public string id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Owner { get; set; } = string.Empty;
}