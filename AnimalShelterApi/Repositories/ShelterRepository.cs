using System.Data;
using AnimalShelterApi.Models;
using Dapper;

namespace AnimalShelterApi.Repositories;

public class ShelterRepository : IShelterRepository
{
    private readonly IDbConnection _dbConnection;

    public ShelterRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public Shelter AddShelter(Shelter shelter)
    {
        var query = "INSERT INTO shelters (Name) VALUES (@Name) RETURNING Id";
        var createdShelter = _dbConnection.QueryFirst<Shelter>(query, shelter);
        return createdShelter;
    }

    public Shelter GetShelterById(Guid id)
    {
        var query = "SELECT * FROM shelters WHERE Id = @Id";
        return _dbConnection.QueryFirstOrDefault<Shelter>(query, new { id });
    }

    public IEnumerable<Shelter> GetAllShelters()
    {
        return _dbConnection.Query<Shelter>("SELECT * FROM shelters").ToList();
    }

    public Shelter GetShelterByName(string name)
    {
        return _dbConnection.QueryFirstOrDefault<Shelter>("SELECT * FROM shelters WHERE Name = @Name",
            new { Name = name });
    }
}