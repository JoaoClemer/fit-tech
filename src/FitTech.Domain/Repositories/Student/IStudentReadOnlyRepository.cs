namespace FitTech.Domain.Repositories.Student
{
    public interface IStudentReadOnlyRepository
    {
        Task<Entities.Student?> GetStudentByEmail(string email);
        Task<Entities.Student?> GetStudentByCPF(string cpf);
        Task<Entities.Student?> GetStudentById(int id);
        Task<Entities.Student?> GetStudentByRegistrationNumber(int registrationNumber);
        Task<bool> IsRegisterNumberUnique(int registrationNumber);
        Task<Entities.Student?> Login(string email, string password);
    }
}
