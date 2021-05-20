using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Employees.API.Entities;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace Employees.API.Data
{
    public class EmployeeContextSeed
    {
        
        public static void SeedData(IMongoCollection<Employee> employeeCollection)
        {
            bool existEmployee = employeeCollection.Find(p => true).Any();

            if (!existEmployee)
            {
                employeeCollection.InsertManyAsync(GetMyEmployees());

            }
        }

        public static IEnumerable<Employee> GetMyEmployees()
        {
            string fileName = "banco2.json";
            string jsonString = File.ReadAllText(fileName);
            CargaInicial cargaInicial = JsonConvert.DeserializeObject<CargaInicial>(jsonString);// JsonConverter<CargaInicial>(jsonString);// JsonSerializer.Deserialize<Object>(jsonString);
            List<Employee> employees = new List<Employee>();

            foreach (var f in cargaInicial.Funcionarios)
            {
                Employee emp = new Employee();
                emp.Matricula = f.Matricula;
                emp.Nome = f.Nome;
                emp.Cargo = f.Cargo;
                emp.Area = f.Area;
                emp.DataAdmissao = Convert.ToDateTime(f.DataAdmissao.Split("-")[2]
                    + "/" + f.DataAdmissao.Split("-")[1]
                    + "/" + f.DataAdmissao.Split("-")[0]);
                emp.SalarioBruto = Convert.ToDouble(f.SalarioBruto);
                employees.Add(emp);

            }
            return employees;
            
        }
    }
}
