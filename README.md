## ASP.NET Core Web API with .NET 8 and EF (Entity Framework) Core 
#### Here’s a quick roadmap to help you implement CRUD operations:

## 1️⃣ Project Setup: 
* Install .NET 8 SDK
* Create a new ASP.NET Core Web API project:
```c#
dotnet new webapi -n EmployeeAPI
cd EmployeeAPI
```
* Install Entity Framework Core packages:

## 2️⃣ Configure Database & Entity Framework Core
* In appsettings.json, add the SQL Server connection string:
```c#
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=EmployeeDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```
* Create a DbContext (Data/ApplicationDbContext.cs)
```c#
using Microsoft.EntityFrameworkCore;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { } 
    public DbSet<Employee> Employees { get; set; }
}
```

* Register DbContext in Program.cs
```c#
builder.Services.AddDbContext<ApplicationDbContext>(Options =>
Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();
```
## 3️⃣ Create Employee Model
* Create a model (Models/Employee.cs)
```c#
public class Employee
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string MobileNo { get; set; }
    public decimal Salary { get; set; }
}
```
## 4️⃣ Migrations & Database Creation
* Run migrations
```c#
dotnet ef migrations add InitialCreate
dotnet ef database update
```
## 5️⃣ Implement CRUD Operations in Controller
* Create Controllers/EmployeeController.cs
```c#
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

// 1️⃣ Get All Employees
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var allemployees = dbContext.Employees.ToList();

            return Ok(allemployees); 
        }

// 2️⃣ Get Employee by ID
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

// 3️⃣ Create Employee
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

 // 4️⃣ Update Employee
  //* All Column With Selected Row
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

  //* Single Column with Selected Row
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

// 5️⃣ Delete Employee
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

    }
}
```
## 6️⃣ Testing API
* Use Postman, Swagger, or cURL to test

➤ Create Employee (POST)
```c#
  POST http://localhost:5000/api/Employee
{
    "name": "John Doe",
    "email": "johndoe@example.com",
    "mobileNo": "1234567890",
    "salary": 60000
}
```

➤ Get All Employees (GET)
```c#
GET http://localhost:5000/api/Employee
```

➤ Get Employee by ID (GET)
```c#
GET http://localhost:5000/api/Employee/{id}
```

➤ Update Employee (PUT)
  ```c#
PUT http://localhost:5000/api/Employee/{id}
{
    "name": "John Smith",
    "email": "johnsmith@example.com",
    "mobileNo": "9876543210",
    "salary": 70000
}
  ```
➤ Delete Employee (DELETE)
  ```c#
DELETE http://localhost:5000/api/Employee/{id}
  ```
## 7️⃣ Run the API
```c#
dotnet run
```
<p>
🚀 By integrating all these components, you can build a robust and fully functional REST API using .NET 8 and Entity Framework Core! 
</p>

<p>
    <em>Let me know if you need any improvements or debugging help. Happy coding!</em> 😃
</p>

<p>
<h5> <em>If you have any suggetions or advice please feel free to connect me </em>:--
</p>
<a href="mailto:anshvnm@gmail.com" target="_blank"><img src="https://img.icons8.com/bubbles/50/000000/gmail.png" alt="Gmail"/></a>
<a href="https://github.com/anshdmishra" target="_blank"><img src="https://img.icons8.com/bubbles/50/000000/github.png" alt="GitHub"/></a>
</h5> 






