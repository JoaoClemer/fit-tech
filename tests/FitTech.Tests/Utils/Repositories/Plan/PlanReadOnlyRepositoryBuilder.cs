using FitTech.Domain.Repositories.Plan;
using Moq;

namespace FitTech.Tests.Utils.Repositories.Plan
{
    public class PlanReadOnlyRepositoryBuilder
    {
        private static PlanReadOnlyRepositoryBuilder _instance;
        private readonly Mock<IPlanReadOnlyRepository> _repository;

        private PlanReadOnlyRepositoryBuilder()
        {
            if (_repository == null)
            {
                _repository = new Mock<IPlanReadOnlyRepository>();
            }
        }

        public static PlanReadOnlyRepositoryBuilder Instance()
        {
            _instance = new PlanReadOnlyRepositoryBuilder();
            return _instance;
        }

        public PlanReadOnlyRepositoryBuilder PlanNameIsInUse(string name, int gymId)
        {
            if (!string.IsNullOrEmpty(name))
                _repository.Setup(i => i.PlanNameIsInUse(name, gymId)).ReturnsAsync(true);

            return this;
        }

        public IPlanReadOnlyRepository Build()
        {
            return _repository.Object;
        }
    }
}
