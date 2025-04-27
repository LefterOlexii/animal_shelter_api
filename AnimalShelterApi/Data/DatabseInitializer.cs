using System.Data;
using Dapper;

namespace AnimalShelterApi.Data
{
    public static class DatabaseInitializer
    {
        public static void EnsureDatabaseCreated(IDbConnection dbConnection)
        {
            var createTablesQuery = @"
                 CREATE TABLE IF NOT EXISTS shelters (
                     Id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
                     Name VARCHAR(255) NOT NULL
                 );

                 CREATE TABLE IF NOT EXISTS animals (
                     Id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
                     Name VARCHAR(255) NOT NULL,
                     Age INT,
                     Breed VARCHAR(255),
                     Gender INT NOT NULL,
                     ShelterId UUID,
                     FOREIGN KEY (ShelterId) REFERENCES shelters(Id)
                 );

                 CREATE TABLE IF NOT EXISTS applications (
                     Id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
                     AnimalId UUID,
                     FOREIGN KEY (AnimalId) REFERENCES animals(Id)
                 );


             ";

            dbConnection.Execute(createTablesQuery);
        }
    }
}