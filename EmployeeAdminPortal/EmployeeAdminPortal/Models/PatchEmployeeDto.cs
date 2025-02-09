namespace EmployeeAdminPortal.Models
{
    public class PatchEmployeeDto
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string? MobileNo { get; set; }
        public decimal Salary { get; set; }
    }
}
