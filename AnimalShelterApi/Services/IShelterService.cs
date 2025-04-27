using AnimalShelterApi.Controllers;

namespace AnimalShelterApi.Services;

public interface IShelterService
{
    Guid AddShelter(ShelterAddRequest request);
    ShelterResponse GetShelterById(Guid id);
    IEnumerable<ShelterResponse> GetAllShelters();
}