using AnimalShelterApi.Controllers;
using AnimalShelterApi.Models;

namespace AnimalShelterApi.Mappers;

public class ShelterMapper
{
    public static ShelterResponse ToResponse(Shelter shelter)
    {
        return new ShelterResponse(shelter.Id, shelter.Name);
    }
}