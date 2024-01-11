using FitTech.Comunication.Enum;
using FitTech.Comunication.Requests.Shared;

namespace FitTech.Comunication.Requests.Employee
{
    public class RequestCreateEmployeeDTO : RequestCreateUserDTO
    {
        public decimal Salary { get; set; }
        public EmployeeTypeDTO EmployeeType { get; set; }
    }
}
