using AnimalShelterApi.Controllers;
using AnimalShelterApi.Models;

namespace AnimalShelterApi.Mappers;

public class ApplicationMapper
{
    public static ApplicationResponse ToResponse(Application application)
    {
        return new ApplicationResponse(application.Id, application.AnimalId);
    }
}