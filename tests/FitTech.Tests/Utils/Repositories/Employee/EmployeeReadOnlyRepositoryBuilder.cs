using FitTech.Domain.Repositories.Employee;
using Moq;

namespace FitTech.Tests.Utils.Repositories.Employee
{
    public class EmployeeReadOnlyRepositoryBuilder
    {
        private static EmployeeReadOnlyRepositoryBuilder _instance;
        private readonly Mock<Domain.Repositories.Employee.IEmployeeReadOnlyRepository> _repository;

        private EmployeeReadOnlyRepositoryBuilder()
        {
            if (_repository == null)
            {
                _repository = new Mock<IEmployeeReadOnlyRepository>();
            }
        }

        public static EmployeeReadOnlyRepositoryBuilder Instance()
        {
            _instance = new EmployeeReadOnlyRepositoryBuilder();
            return _instance;
        }

        public EmployeeReadOnlyRepositoryBuilder GetEmployeeByEmail(string email)
        {
            if (!string.IsNullOrEmpty(email))
                _repository.Setup(i => i.GetEmployeeByEmail(email)).ReturnsAsync(new Domain.Entities.Employee());

            return this;
        }

        public EmployeeReadOnlyRepositoryBuilder GetEmployeeByCPF(string CPF)
        {
            if (!string.IsNullOrEmpty(CPF))
                _repository.Setup(i => i.GetEmployeeByCPF(CPF)).ReturnsAsync(new Domain.Entities.Employee());

            return this;
        }

        public EmployeeReadOnlyRepositoryBuilder GetEmployeeById(int id)
        {
            if (id > 0)
                _repository.Setup(i => i.GetEmployeeById(id)).ReturnsAsync(new Domain.Entities.Employee());

            return this;
        }

        public IEmployeeReadOnlyRepository Build()
        {
            return _repository.Object;
        }
    }
}

