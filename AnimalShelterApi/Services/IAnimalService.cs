using AnimalShelterApi.Controllers;

namespace AnimalShelterApi.Services;

public interface IAnimalService
{
    Guid AddAnimal(AnimalAddRequest request);
    AnimalResponse GetAnimalById(Guid id);
    IEnumerable<AnimalResponse> GetAllAnimals();
    IEnumerable<AnimalResponse> GetAnimalsWithoutApplications(Guid? shelterId);
}