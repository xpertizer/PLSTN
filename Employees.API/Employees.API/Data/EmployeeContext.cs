using System;
using Employees.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Employees.API.Data
{
    public class EmployeeContext : IEmployeeContext
    {
        public EmployeeContext(IConfiguration configuration)
        {
            var employee = new MongoClient(configuration.GetValue<string>
                ("DatabaseSettings:ConnectionString"));

            var database = employee.GetDatabase(configuration.GetValue<string>
                ("DatabaseSettings:DatabaseName"));

            Employees = database.GetCollection<Employee>(configuration.GetValue<string>
                ("DatabaseSettings:CollectionName"));

            EmployeeContextSeed.SeedData(Employees);
        }

        public IMongoCollection<Employee> Employees { get; }
    }
}
