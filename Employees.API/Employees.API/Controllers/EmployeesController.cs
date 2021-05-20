using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Employees.API.Entities;
using Employees.API.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Employees.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _repository;

        public EmployeesController(IEmployeeRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            var employees = await _repository.GetEmployees();
            return Ok(employees);
        }

        [HttpGet("{matricula:length(7)}", Name = "GetEmployee")]
        public async Task<ActionResult<Employee>> GetEmployeeByMatricula(string matricula)
        {
            var employee = await _repository.GetEmployee(matricula);

            if (employee is null)
            {
                return NotFound();
            }
            return Ok(employee);
        }
        [Route("[action]/{area}", Name = "GetEmployeeByArea")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeeByArea(string area)
        {
            if (area is null)
                return BadRequest("Invalid Area");
            var employees = await _repository.GetEmployeeByArea(area);

            return Ok(employees);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee([FromBody] Employee employee)
        {
            if (employee is null)
                return BadRequest("Invalid Employee");

            await _repository.CreateEmployee(employee);

            return CreatedAtRoute("GetEmployee", new { matricula = employee.Matricula }, employee);
        }
        [Route("[action]", Name ="ProfitSharing")]
        [HttpPost]
        public async Task<ActionResult<ProfitSharingResponse>> ProfitSharing([FromBody] ProfitSharingRequest request)
        {

            if (request is null)
                return BadRequest("Invalid Request");
            var response = await _repository.ProfitSharing(request);
            return (StatusCode(200, response));



        }

    }
}
