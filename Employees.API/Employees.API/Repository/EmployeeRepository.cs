using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Employees.API.Data;
using Employees.API.Entities;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace Employees.API.Repository
{
    public class EmployeeRepository:IEmployeeRepository
    {
        private readonly IEmployeeContext _context;
        public EmployeeRepository(IEmployeeContext context)
        {
            _context = context ??
                throw new ArgumentException(nameof(context));
        }

        public async Task CreateEmployee(Employee employee)
        {
            await _context.Employees.InsertOneAsync(employee);
        }

        public async Task<bool> DeleteEmployee(string matricula)
        {
            FilterDefinition<Employee> filter = Builders<Employee>.Filter.Eq(p => p.Matricula, matricula);

            DeleteResult deleteResult = await _context.Employees.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        public async Task<Employee> GetEmployee(string matricula)
        {
            return await _context.Employees.Find(p => p.Matricula == matricula).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Employee>> GetEmployeeByArea(string area)
        {
            FilterDefinition<Employee> filter = Builders<Employee>.Filter
                .Eq(p => p.Area, area);

            return await _context.Employees.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetEmployeeByName(string name)
        {
            FilterDefinition<Employee> filter = Builders<Employee>.Filter
                .Eq(p => p.Nome, name);

            return await _context.Employees.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _context.Employees.Find(p => true).ToListAsync();
        }

        public async Task<bool> UpdateEmployee(Employee employee)
        {
            var updateResult = await _context.Employees.ReplaceOneAsync(
                filter: g => g.Matricula == employee.Matricula, replacement: employee);

            return updateResult.IsAcknowledged
                && updateResult.ModifiedCount > 0;
        }

        public async Task<ProfitSharingResponse> ProfitSharing(ProfitSharingRequest request)
        {

            ProfitSharingResponse response = new ProfitSharingResponse();
            List<Employee> employees = await _context.Employees.Find(p => true).ToListAsync();
            ProfitSharingRules pfsr = new ProfitSharingRules();

            return pfsr.ProfitSharingReport(request, employees);

        }

        public List<Employee> CargaInicial()
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
