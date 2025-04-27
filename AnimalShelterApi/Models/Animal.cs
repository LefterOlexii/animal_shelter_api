namespace AnimalShelterApi.Models;

public enum Gender
{
    Male,
    Female
}

public class Animal
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int? Age { get; set; }
    public string? Breed { get; set; }
    public Gender Gender { get; set; }
    public Guid ShelterId { get; set; }
}