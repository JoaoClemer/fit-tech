using FitTech.Domain.Repositories.Student;
using Moq;

namespace FitTech.Tests.Utils.Repositories.Student
{
    public class StudentUpdateOnlyRepositoryBuilder
    {
        private static StudentUpdateOnlyRepositoryBuilder _instance;
        private readonly Mock<Domain.Repositories.Student.IStudentUpdateOnlyRepository> _repository;

        private StudentUpdateOnlyRepositoryBuilder()
        {
            if (_repository == null)
            {
                _repository = new Mock<IStudentUpdateOnlyRepository>();
            }
        }

        public static StudentUpdateOnlyRepositoryBuilder Instance()
        {
            _instance = new StudentUpdateOnlyRepositoryBuilder();
            return _instance;
        }

        public IStudentUpdateOnlyRepository Build()
        {
            return _repository.Object;
        }
    }
}
