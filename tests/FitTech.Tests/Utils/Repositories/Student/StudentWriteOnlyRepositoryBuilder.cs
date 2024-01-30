using FitTech.Domain.Repositories.Student;
using Moq;

namespace FitTech.Tests.Utils.Repositories.Student
{
    public class StudentWriteOnlyRepositoryBuilder
    {
        private static StudentWriteOnlyRepositoryBuilder _instance;
        private readonly Mock<IStudentWriteOnlyRepository> _repository;

        private StudentWriteOnlyRepositoryBuilder()
        {
            if (_repository == null)
            {
                _repository = new Mock<IStudentWriteOnlyRepository>();
            }
        }

        public static StudentWriteOnlyRepositoryBuilder Instance()
        {
            _instance = new StudentWriteOnlyRepositoryBuilder();
            return _instance;
        }

        public IStudentWriteOnlyRepository Build()
        {
            return _repository.Object;
        }
    }
}
