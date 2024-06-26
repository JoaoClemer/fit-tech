﻿using FitTech.Domain.Entities;
using FitTech.Domain.Repositories.Student;
using FitTech.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace FitTech.Infrastructure.RepositoryAccess
{
    public class StudentRepository : IStudentReadOnlyRepository, IStudentWriteOnlyRepository, IStudentUpdateOnlyRepository
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

        public async Task<ICollection<Student>> GetAllStudentsOfGym(int gymId)
        {
            return await _context.Students
                .AsNoTracking()
                .Include(x => x.StudentPlan)
                .Where(s => s.Gym.Id.Equals(gymId))
                .ToListAsync();
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
                .Include(s => s.Gym)
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

        public void Update(Student student)
        {
            _context.Students.Update(student);
        }
    }
}
