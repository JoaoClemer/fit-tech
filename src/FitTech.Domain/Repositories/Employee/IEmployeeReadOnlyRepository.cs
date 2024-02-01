namespace FitTech.Domain.Repositories.Employee
{
    public interface IEmployeeReadOnlyRepository
    {
        Task<Entities.Employee?> GetEmployeeByEmail(string email);
        Task<Entities.Employee?> GetEmployeeByCPF(string cpf);
        Task<Entities.Employee?> GetEmployeeById(int id);
        Task<Entities.Employee?> Login(string email, string password);
    }
}
