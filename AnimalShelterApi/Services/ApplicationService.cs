using AnimalShelterApi.Controllers;
using AnimalShelterApi.Exceptions;
using AnimalShelterApi.Mappers;
using AnimalShelterApi.Models;
using AnimalShelterApi.Repositories;

namespace AnimalShelterApi.Services;

public class ApplicationService : IApplicationService
{
    private readonly IApplicationRepository _applicationRepository;
    private readonly IAnimalRepository _animalRepository;

    public ApplicationService(IApplicationRepository applicationRepository, IAnimalRepository animalRepository)
    {
        _applicationRepository = applicationRepository;
        _animalRepository = animalRepository;
    }

    public IEnumerable<ApplicationResponse> GetAllApplications()
    {
        var applications = _applicationRepository.GetAllApplications();
        return applications.Select(ApplicationMapper.ToResponse);
    }

    public ApplicationResponse GetApplicationById(Guid id)
    {
        var application = _applicationRepository.GetApplicationById(id);

        if (application == null)
        {
            throw new NotFoundException($"Application with Id {id} not found");
        }

        return ApplicationMapper.ToResponse(application);
    }

    public Guid AddApplication(ApplicationAddRequest request)
    {
        var existingAnimal = _animalRepository.GetAnimalById(request.AnimalId);
        if (existingAnimal == null)
        {
            throw new InvalidOperationException("A animal with that id does not exist.");
        }

        var application = new Application
        {
            AnimalId = request.AnimalId
        };
        var addedApplication = _applicationRepository.AddApplication(application);

        return addedApplication.Id;
    }
}