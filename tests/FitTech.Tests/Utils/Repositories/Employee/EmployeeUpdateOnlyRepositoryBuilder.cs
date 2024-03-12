using FitTech.Domain.Repositories.Employee;
using Moq;

namespace FitTech.Tests.Utils.Repositories.Employee
{
    public class EmployeeUpdateOnlyRepositoryBuilder
    {
        private static EmployeeUpdateOnlyRepositoryBuilder _instance;
        private readonly Mock<Domain.Repositories.Employee.IEmployeeUpdateOnlyRepository> _repository;

        private EmployeeUpdateOnlyRepositoryBuilder()
        {
            if (_repository == null)
            {
                _repository = new Mock<IEmployeeUpdateOnlyRepository>();
            }
        }

        public static EmployeeUpdateOnlyRepositoryBuilder Instance()
        {
            _instance = new EmployeeUpdateOnlyRepositoryBuilder();
            return _instance;
        }

        public IEmployeeUpdateOnlyRepository Build()
        {
            return _repository.Object;
        }
    }
}
