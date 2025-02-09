using System.Diagnostics.CodeAnalysis;

namespace EmployeeAdminPortal.Models.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string? MobileNo { get; set; }
        public decimal Salary { get; set; }
        

    }
}
