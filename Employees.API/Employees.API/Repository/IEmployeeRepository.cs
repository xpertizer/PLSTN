using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Employees.API.Entities;

namespace Employees.API.Repository
{
    public interface IEmployeeRepository
    {

        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployee(string matricula);
        Task<IEnumerable<Employee>> GetEmployeeByName(string name);
        Task<IEnumerable<Employee>> GetEmployeeByArea(string area);

        Task CreateEmployee(Employee employee);
        Task<bool> UpdateEmployee(Employee employee);
        Task<bool> DeleteEmployee(string matricula);
        Task<ProfitSharingResponse> ProfitSharing(ProfitSharingRequest request);


    }

    

}
