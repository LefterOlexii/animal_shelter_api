using AnimalShelterApi.Models;

namespace AnimalShelterApi.Repositories;

public interface IApplicationRepository
{
    Application AddApplication(Application application);
    Application? GetApplicationById(Guid id);
    IEnumerable<Application> GetAllApplications();
}