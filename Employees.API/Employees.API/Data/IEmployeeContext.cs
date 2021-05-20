using System;
using Employees.API.Entities;
using MongoDB.Driver;

namespace Employees.API.Data
{
    public interface IEmployeeContext
    {
        IMongoCollection<Employee> Employees { get; }
    }
}
