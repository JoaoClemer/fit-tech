using FitTech.Domain.Repositories.Plan;
using Moq;

namespace FitTech.Tests.Utils.Repositories.Plan
{
    public class PlanWriteOnlyRepositoryBuilder
    {
        private static PlanWriteOnlyRepositoryBuilder _instance;
        private readonly Mock<IPlanWriteOnlyRepository> _repository;

        private PlanWriteOnlyRepositoryBuilder()
        {
            if (_repository == null)
            {
                _repository = new Mock<IPlanWriteOnlyRepository>();
            }
        }

        public static PlanWriteOnlyRepositoryBuilder Instance()
        {
            _instance = new PlanWriteOnlyRepositoryBuilder();
            return _instance;
        }

        public IPlanWriteOnlyRepository Build()
        {
            return _repository.Object;
        }
    }
}
