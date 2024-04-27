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

        public StudentReadOnlyRepositoryBuilder GetAllStudentsOfGym(int gymId)
        {
            if (gymId > 0)
            {
                var studentList = new List<Domain.Entities.Student>
                {
                    new Domain.Entities.Student
                    {
                        Name = "João",
                        StudentPlan = new Domain.Entities.StudentPlan
                        {
                            IsActive = true,
                            Plan = new Domain.Entities.Plan
                            {
                                Price = 100
                            }
                        }
                    },
                    new Domain.Entities.Student
                    {
                        Name = "Pedro",
                        StudentPlan = new Domain.Entities.StudentPlan
                        {
                            IsActive = false,
                            Plan = new Domain.Entities.Plan
                            {
                                Price = 100
                            }
                        }
                    },
                };

                _repository.Setup(i => i.GetAllStudentsOfGym(gymId)).ReturnsAsync(studentList);

            }

            return this;
        }

        public StudentReadOnlyRepositoryBuilder Login(string emailAddress, string password)
        {
            if (!string.IsNullOrEmpty(emailAddress) && !string.IsNullOrEmpty(password))
                _repository.Setup(i => i.Login(emailAddress, password)).ReturnsAsync(new Domain.Entities.Student
                {
                    Name = "UseCaseTest",
                    EmailAddress = "usecase@test.com"
                });

            return this;
        }

        public IStudentReadOnlyRepository Build()
        {
            return _repository.Object;
        }
    }
}
