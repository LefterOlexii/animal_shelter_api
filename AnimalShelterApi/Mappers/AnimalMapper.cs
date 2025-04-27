using AnimalShelterApi.Controllers;
using AnimalShelterApi.Models;

namespace AnimalShelterApi.Mappers;

public class AnimalMapper
{
    public static AnimalResponse ToResponse(Animal animal)
    {
        return new AnimalResponse(animal.Id, animal.Name, animal.Age, animal.Breed, animal.Gender, animal.ShelterId);
    }
}