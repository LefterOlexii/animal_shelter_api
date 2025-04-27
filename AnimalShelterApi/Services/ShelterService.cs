using AnimalShelterApi.Controllers;
using AnimalShelterApi.Exceptions;
using AnimalShelterApi.Extensions;
using AnimalShelterApi.Mappers;
using AnimalShelterApi.Models;
using AnimalShelterApi.Repositories;
using AnimalShelterApi.Validators;

namespace AnimalShelterApi.Services;

public class ShelterService : IShelterService
{
    private readonly IShelterRepository _repository;
    private readonly ShelterValidator _validator;
    
    public ShelterService(IShelterRepository repository)
    {
        _repository = repository;
        _validator = new ShelterValidator();
    }
    
    public ShelterResponse GetShelterById(Guid id)
    {
        var shelter = _repository.GetShelterById(id);

        if (shelter == null)
        {
            throw new NotFoundException($"Shelter with Id {id} not found");
        }
        return ShelterMapper.ToResponse(shelter);
    }

    public Guid AddShelter(ShelterAddRequest request)
    {
        var existingShelter = _repository.GetShelterByName(request.Name);
        if (existingShelter != null)
        {
            throw new InvalidOperationException("A shelter with that name already exists.");
        }

        var shelter = new Shelter
        {
            Name = request.Name,
        };
        
        var validationResult = _validator.Validate(shelter);
        validationResult.ThrowIfFail();
        
        var addedShelter = _repository.AddShelter(shelter);
        
        return addedShelter.Id;
    }
    
    public IEnumerable<ShelterResponse> GetAllShelters()
    {
        var shelters = _repository.GetAllShelters();
        return shelters.Select(ShelterMapper.ToResponse);
    }
    
}