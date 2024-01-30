using FitTech.Domain.Repositories.Student;
using Moq;

namespace FitTech.Tests.Utils.Repositories.Student
{
    internal class StudentReadOnlyRepositoryBuilder
    {
        private static StudentReadOnlyRepositoryBuilder _instance;
        private readonly Mock<IStudentReadOnlyRepository> _repository;

        private StudentReadOnlyRepositoryBuilder()
        {
            if (_repository == null)
            {
                _repository = new Mock<IStudentReadOnlyRepository>();
            }
        }

        public static StudentReadOnlyRepositoryBuilder Instance()
        {
            _instance = new StudentReadOnlyRepositoryBuilder();
            return _instance;
        }

        public StudentReadOnlyRepositoryBuilder GetStudentByEmail(string email)
        {
            if (!string.IsNullOrEmpty(email))
                _repository.Setup(i => i.GetStudentByEmail(email)).ReturnsAsync(new Domain.Entities.Student());

            return this;
        }

        public StudentReadOnlyRepositoryBuilder GetStudentByCPF(string CPF)
        {
            if (!string.IsNullOrEmpty(CPF))
                _repository.Setup(i => i.GetStudentByCPF(CPF)).ReturnsAsync(new Domain.Entities.Student());

            return this;
        }

        public StudentReadOnlyRepositoryBuilder GetStudentById(int id)
        {
            if (id > 0)
                _repository.Setup(i => i.GetStudentById(id)).ReturnsAsync(new Domain.Entities.Student());

            return this;
        }

        public StudentReadOnlyRepositoryBuilder IsRegisterNumberUnique(int registerNumber)
        {
            if (registerNumber > 0)
                _repository.Setup(i => i.IsRegisterNumberUnique(registerNumber)).ReturnsAsync(false);

            return this;
        }

        public IStudentReadOnlyRepository Build()
        {
            return _repository.Object;
        }
    }
}
