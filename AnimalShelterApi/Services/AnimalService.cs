using AnimalShelterApi.Controllers;
using AnimalShelterApi.Exceptions;
using AnimalShelterApi.Extensions;
using AnimalShelterApi.Mappers;
using AnimalShelterApi.Models;
using AnimalShelterApi.Repositories;
using AnimalShelterApi.Validators;

namespace AnimalShelterApi.Services;

public class AnimalService : IAnimalService
{
    private readonly IAnimalRepository _repository;
    private readonly IShelterRepository _shelterRepository;
    private readonly AnimalValidator _validator;

    public AnimalService(IAnimalRepository repository, IShelterRepository shelterRepository)
    {
        _repository = repository;
        _shelterRepository = shelterRepository;
        _validator = new AnimalValidator();
    }

    public Guid AddAnimal(AnimalAddRequest request)
    {
        var existingShelter = _shelterRepository.GetShelterById(request.ShelterId);
        if (existingShelter == null)
        {
            throw new InvalidOperationException("A shelter with that id does not exist.");
        }


        var animal = new Animal
        {
            Name = request.Name,
            Age = request.Age,
            Breed = request.Breed,
            Gender = request.Gender,
            ShelterId = request.ShelterId
        };

        var validationResult = _validator.Validate(animal);
        validationResult.ThrowIfFail();

        var addedShelter = _repository.AddAnimal(animal);

        return addedShelter.Id;
    }

    public AnimalResponse GetAnimalById(Guid id)
    {
        var animal = _repository.GetAnimalById(id);

        if (animal == null)
        {
            throw new NotFoundException($"Animal with Id {id} not found");
        }

        return AnimalMapper.ToResponse(animal);
    }

    public IEnumerable<AnimalResponse> GetAllAnimals()
    {
        var animals = _repository.GetAllAnimals();
        return animals.Select(AnimalMapper.ToResponse);
    }

    public IEnumerable<AnimalResponse> GetAnimalsWithoutApplications(Guid? shelterId)
    {
        var animals = _repository.GetAnimalsWithoutApplications(shelterId);
        return animals.Select(AnimalMapper.ToResponse).ToList();
    }
}