using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAdminPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public EmployeesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var allemployees = dbContext.Employees.ToList();

            return Ok(allemployees); 
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployeeByID(Guid id)
        {
            var employee = dbContext.Employees.Find(id);
            if(employee is null)
            {
                return NotFound("No Record Found, Please Try With Another ID");          
            }
            return Ok(employee);
        }


        [HttpPost]
        public IActionResult AddEmployee(AddEmployeeDto addEmployeeDto)
        {
            var employeeEntity = new Employee()
            {
                Name = addEmployeeDto.Name,
                Email = addEmployeeDto.Email,
                MobileNo = addEmployeeDto.MobileNo,
                Salary = addEmployeeDto.Salary
            };

            dbContext.Employees.Add(employeeEntity);
            dbContext.SaveChanges();

            return Ok(employeeEntity);
        }


        [HttpPut]
        [Route ("{id:guid}")]
        public IActionResult UpdateEmployee(Guid id, UpdateEmployeeDto updateEmployeeDto) 
        {
            var employee= dbContext.Employees.Find(id);
            if (employee is null)
            {
                return NotFound("Record Not Found");
            }
            employee.Name = updateEmployeeDto.Name;
            employee.Email = updateEmployeeDto.Email;
            employee.MobileNo = updateEmployeeDto.MobileNo;
            employee.Salary = updateEmployeeDto.Salary;

            dbContext.SaveChanges ();

            return Ok(employee);
        }


        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteEmployee(Guid id) 
        {
            var employee = dbContext.Employees.Find(id);
            if(employee is null)
            {
                return NotFound("No Rcord Found");
            }
            dbContext.Employees.Remove(employee);
            dbContext.SaveChanges();

            return Ok("Record Removed");
        }


        [HttpPatch]
        [Route("{id:guid}")]
        public IActionResult PatchEmployee (Guid id, PatchEmployeeDto patchEmployeeDto)
        {
            var employee = dbContext.Employees.Find (id);
            if (employee is null)
            {
                return NotFound("Record Not Found");
            }
            employee.Name = patchEmployeeDto.Name;
            employee.Email = patchEmployeeDto.Email;
            employee.MobileNo = patchEmployeeDto.MobileNo;
            employee.Salary = patchEmployeeDto.Salary;

            dbContext.SaveChanges () ;
            return Ok(employee);
        }
    }
}
