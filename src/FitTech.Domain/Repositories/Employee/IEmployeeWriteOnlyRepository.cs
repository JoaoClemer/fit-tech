namespace FitTech.Domain.Repositories.Employee
{
    public interface IEmployeeWriteOnlyRepository
    {
        Task CreateEmployee(Entities.Employee employee);
    }
}
