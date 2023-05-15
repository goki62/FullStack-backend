using backendApi.Data;
using backendApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backendApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly backendDbContext _backendDbContext;

        public EmployeesController(backendDbContext BackendDbContext)
        {
            this._backendDbContext = BackendDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
           var employees = await _backendDbContext.Employees.ToListAsync();

            return Ok(employees);
        }

        [HttpPost]

        public async Task<IActionResult> AddEmployees([FromBody]Employee employeeRequest) 
        {
            employeeRequest.Id = Guid.NewGuid();

           await _backendDbContext.Employees.AddAsync(employeeRequest);
           await _backendDbContext.SaveChangesAsync();

           return Ok(employeeRequest);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute]Guid id) 
        {
            var employee = await _backendDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee == null) 
            {
                return NotFound();
            }
            return Ok(employee); 
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, Employee updateEmployeeReq)
        {
         var employee = await _backendDbContext.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            employee.Name= updateEmployeeReq.Name;
            employee.Email = updateEmployeeReq.Email;
            employee.Phone = updateEmployeeReq.Phone;
            employee.Salary = updateEmployeeReq.Salary;
            employee.Department = updateEmployeeReq.Department;

            await _backendDbContext.SaveChangesAsync();
                
            return Ok(employee);
        }
        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var employee = await _backendDbContext.Employees.FindAsync(id);

            if(employee == null)
            {
                return NotFound();
            }
            _backendDbContext.Employees.Remove(employee);
            await _backendDbContext.SaveChangesAsync();

            return Ok(employee);
        }
    }
}
