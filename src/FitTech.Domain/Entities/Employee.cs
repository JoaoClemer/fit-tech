using FitTech.Domain.Enum;

namespace FitTech.Domain.Entities
{
    public class Employee : User
    {
        public decimal Salary { get; set; }
        public EmployeeType EmployeeType { get; set; }
    }

}
