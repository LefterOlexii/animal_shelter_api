using AnimalShelterApi.Controllers;
using AnimalShelterApi.Models;

namespace AnimalShelterApi.Services;

public interface IApplicationService
{
    Guid AddApplication(ApplicationAddRequest request);
    ApplicationResponse GetApplicationById(Guid id);
    IEnumerable<ApplicationResponse> GetAllApplications();
}