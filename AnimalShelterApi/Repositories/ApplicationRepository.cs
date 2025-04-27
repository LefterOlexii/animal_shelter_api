using System.Data;
using AnimalShelterApi.Models;
using Dapper;

namespace AnimalShelterApi.Repositories;

public class ApplicationRepository : IApplicationRepository
{
    private readonly IDbConnection _dbConnection;

    public ApplicationRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public Application AddApplication(Application application)
    {
        var query = @"
                INSERT INTO applications (AnimalId) VALUES (@AnimalId) RETURNING Id, AnimalId";
        var createdApplication = _dbConnection.QueryFirst<Application>(query, application);
        return createdApplication;
    }

    public IEnumerable<Application> GetAllApplications()
    {
        var query = "SELECT * FROM applications";
        return _dbConnection.Query<Application>(query).ToList();
    }

    public Application GetApplicationById(Guid id)
    {
        var query = "SELECT * FROM animals WHERE Id = @Id";
        return _dbConnection.QueryFirstOrDefault<Application>(query, new { id });
    }
}