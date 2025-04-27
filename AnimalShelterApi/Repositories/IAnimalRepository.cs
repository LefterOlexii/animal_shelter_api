using AnimalShelterApi.Models;

namespace AnimalShelterApi.Repositories;

public interface IAnimalRepository
{
    Animal AddAnimal(Animal animal);
    Animal? GetAnimalById(Guid id);
    IEnumerable<Animal> GetAnimalsWithoutApplications(Guid? shelterId);
    IEnumerable<Animal> GetAllAnimals();
}