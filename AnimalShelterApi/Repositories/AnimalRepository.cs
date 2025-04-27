using Dapper;
using AnimalShelterApi.Models;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace AnimalShelterApi.Repositories;

public class AnimalRepository : IAnimalRepository
{
    private readonly IDbConnection _dbConnection;

    public AnimalRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public Animal AddAnimal(Animal animal)
    {
        var query = @"
                INSERT INTO animals (Name, Age, Breed, Gender, ShelterId)
                VALUES (@Name, @Age, @Breed, @Gender, @ShelterId)
                RETURNING Id, Name, Age, Breed, Gender, ShelterId";
        var createdAnimal = _dbConnection.QueryFirst<Animal>(query, animal);
        return createdAnimal;
    }

    public IEnumerable<Animal> GetAllAnimals()
    {
        var query = "SELECT * FROM animals";
        return _dbConnection.Query<Animal>(query).ToList();
    }

    public Animal GetAnimalById(Guid id)
    {
        var query = "SELECT * FROM animals WHERE Id = @Id";
        return _dbConnection.QueryFirstOrDefault<Animal>(query, new { id });
    }

    public IEnumerable<Animal> GetAnimalsWithoutApplications(Guid? shelterId = null)
    {
        var query = @"
        SELECT * FROM animals a
        WHERE NOT EXISTS (
            SELECT 1 FROM applications app WHERE app.AnimalId = a.Id
        )";

        if (shelterId.HasValue)
        {
            query += " AND a.ShelterId = @ShelterId";
        }

        return _dbConnection.Query<Animal>(query, new { ShelterId = shelterId }).ToList();
    }
}