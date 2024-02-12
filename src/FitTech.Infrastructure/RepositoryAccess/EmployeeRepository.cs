using FitTech.Domain.Entities;
using FitTech.Domain.Repositories.Employee;
using FitTech.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace FitTech.Infrastructure.RepositoryAccess
{
    public class EmployeeRepository : IEmployeeReadOnlyRepository, IEmployeeWriteOnlyRepository
    {
        private readonly FitTechContext _context;

        public EmployeeRepository(FitTechContext context)
        {
            _context = context;
        }
        public async Task CreateEmployee(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
        }

        public async Task<Employee?> GetEmployeeByCPF(string cpf)
        {
            return await _context.Employees.
                AsNoTracking().
                FirstOrDefaultAsync( e => e.Cpf.Equals(cpf) );
        }

        public async Task<Employee?> GetEmployeeByEmail(string email)
        {
            return await _context.Employees.
                AsNoTracking().
                FirstOrDefaultAsync( e => e.EmailAddress.Equals(email) );
        }

        public async Task<Employee?> GetEmployeeById(int id)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public async Task<Employee?> Login(string email, string password)
        {
            return await _context.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.EmailAddress.Equals(email) && e.Password.Equals(password));
        }
    }
}
