using FitTech.Domain.Entities;
using FitTech.Domain.Repositories.Student;
using FitTech.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace FitTech.Infrastructure.RepositoryAccess
{
    public class StudentRepository : IStudentReadOnlyRepository, IStudentWriteOnlyRepository
    {
        private readonly FitTechContext _context;

        public StudentRepository(FitTechContext context)
        {
            _context = context;
        }

        public async Task CreateStudent(Student student)
        {
            await _context.Students.AddAsync(student);
        }

        public async Task<Student?> GetStudentByCPF(string cpf)
        {
            return await _context.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Cpf.Equals(cpf));
        }

        public async Task<Student?> GetStudentByEmail(string email)
        {
            return await _context.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.EmailAddress.Equals(email));
        }

        public async Task<Student?> GetStudentById(int id)
        {
            return await _context.Students
                .FirstOrDefaultAsync(s => s.Id.Equals(id));
        }

        public async Task<Student?> GetStudentByRegistrationNumber(int registrationNumber)
        {
            return await _context.Students
                .FirstOrDefaultAsync(s => s.RegistrationNumber.Equals(registrationNumber));
        }

        public async Task<bool> IsRegisterNumberUnique(int registrationNumber)
        {
            return await _context.Students
                .AsNoTracking()
                .AnyAsync(s => s.RegistrationNumber.Equals(registrationNumber));
        }

        public async Task<Student?> Login(string email, string password)
        {
            return await _context.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.EmailAddress.Equals(email) && s.Password.Equals(password));
        }
    }
}
