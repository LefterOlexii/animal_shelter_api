using AnimalShelterApi.Models;

namespace AnimalShelterApi.Repositories;

public interface IShelterRepository
{
    Shelter AddShelter(Shelter shelter);
    Shelter? GetShelterById(Guid id);
    IEnumerable<Shelter> GetAllShelters();
    Shelter GetShelterByName(string name);
}