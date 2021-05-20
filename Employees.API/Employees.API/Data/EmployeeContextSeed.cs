using System;
using System.Collections.Generic;
using Employees.API.Entities;
using MongoDB.Driver;

namespace Employees.API.Data
{
    public class EmployeeContextSeed
    {
        public EmployeeContextSeed()
        {
        }
        public static void SeedData(IMongoCollection<Employee> employeeCollection)
        {
            bool existEmployee = employeeCollection.Find(p => true).Any();

            if (!existEmployee)
            {
                employeeCollection.InsertManyAsync(GetMyEmployees());

            }
        }

        private static IEnumerable<Employee> GetMyEmployees()
        {
            return new List<Employee>
            {
                new Employee()
                {
                    Matricula = "11111",
                    Nome = "Leonardo",
                    Area = "Tesouraria",
                    Cargo = "Analista",
                    SalarioBruto = 1000000,
                    DataAdmissao = Convert.ToDateTime("01/01/2021")
                }
            };
        }
    }
}
