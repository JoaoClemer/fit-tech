using FitTech.Domain.Repositories.Employee;
using Moq;

namespace FitTech.Tests.Utils.Repositories.Employee
{
    public class EmployeeWriteOnlyRepositoryBuilder
    {
        private static EmployeeWriteOnlyRepositoryBuilder _instance;
        private readonly Mock<IEmployeeWriteOnlyRepository> _repository;

        private EmployeeWriteOnlyRepositoryBuilder()
        {
            if (_repository == null)
            {
                _repository = new Mock<IEmployeeWriteOnlyRepository>();
            }
        }

        public static EmployeeWriteOnlyRepositoryBuilder Instance()
        {
            _instance = new EmployeeWriteOnlyRepositoryBuilder();
            return _instance;
        }

        public IEmployeeWriteOnlyRepository Build()
        {
            return _repository.Object;
        }
    }
}
