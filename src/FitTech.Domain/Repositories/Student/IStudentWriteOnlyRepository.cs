namespace FitTech.Domain.Repositories.Student
{
    public interface IStudentWriteOnlyRepository
    {
        Task CreateStudent(Entities.Student student);
    }
}
