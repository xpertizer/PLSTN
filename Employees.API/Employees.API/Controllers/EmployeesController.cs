using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Employees.API.Entities;
using Employees.API.Repository;
using Microsoft.AspNetCore.Http;
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

        /// <summary>
        /// Get Employee List.
        /// If Employees Collection in MongoDB was empty, this method load a banco2.json file and include automatically in Employees Collection
        /// </summary>
        /// <returns>A employee List</returns>
        /// <response code="200">Request Succesfully</response>
        [Produces("application/json")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            var employees = await _repository.GetEmployees();
            return Ok(employees);
        }

        /// <summary>
        /// Get Employee List Filtered by Matricula.
        /// </summary>
        /// /// <returns>A employee List filtered by matricula field</returns>
        /// <response code="200">Request OK</response>
        /// <response code="404">Matricula Not Found</response>
        [Produces("application/json")]
        [HttpGet("{matricula:length(7)}", Name = "GetEmployee")]
        public async Task<ActionResult<Employee>> GetEmployeeByMatricula(string matricula)
        {
            var employee = await _repository.GetEmployee(matricula);

            if (employee is null)
            {
                return NotFound("Matricula Not Found");
            }
            return Ok(employee);
        }

        /// <summary>
        /// Get Employee List Filtered by Area.
        /// </summary>
        /// <returns>A employee List filtered by Area field</returns>
        /// <response code="200">Request OK</response>
        /// <response code="404">Invalid Area</response>
        /// [Route("[action]/{area}", Name = "GetEmployeeByArea")]
        [Produces("application/json")]
        [HttpGet("{area}")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeeByArea(string area)
        {
            if (area is null)
                return BadRequest("Invalid Area");
            var employees = await _repository.GetEmployeeByArea(area);

            return Ok(employees);
        }


        /// <summary>
        /// Creates a Employee.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Employee
        ///     {
        ///          "matricula": "0004468",
        ///          "nome": "JohnDoe Gates",
        ///          "area": "Accounting",
        ///          "cargo": "Analyst",
        ///          "salario_bruto": "3.245,52",
        ///          "data_de_admissao": "2010-10-07"
        ///     }
        ///
        /// </remarks>
        /// <param name="employee"></param>
        /// <returns>A newly created Employee</returns>
        /// <response code="201">Returns the newly created employee</response>
        /// <response code="400">Invalid Employee</response>      
        [Produces("application/json")]
        [HttpPost]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Employee>> CreateEmployee([FromBody] Employee employee)
        {
            if (employee is null)
                return BadRequest("Invalid Employee");

            await _repository.CreateEmployee(employee);

            return CreatedAtRoute("GetEmployee", new { matricula = employee.Matricula }, employee);
        }

        /// <summary>
        /// Creates a Profit Sharing Report.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /ProfitSharingRequest
        ///     {
        ///          "ValorMaximoADistribuir": 10000000,
        ///          "SalarioMinimoAtual": 900
        ///     }
        ///
        /// </remarks>
        /// <param name="ValorMaximoADistribuir">Value to be shared with emplyees</param>
        /// <param name="SalarioMinimoAtual">Value of minimal wage to be used in calculation</param>
        /// <returns>A report in Json ProfitSharingResponse</returns>
        /// <response code="201">Returns Json with all employee Profit and summaries in the end</response>
        /// <response code="400">Invalid Request</response>
        [Produces("Application/Json")]
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
