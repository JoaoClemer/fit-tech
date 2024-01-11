using FitTech.Domain.Repositories.Gym;
using Moq;

namespace FitTech.Tests.Utils.Repositories
{
    public class GymWriteOnlyRepositoryBuilder
    {
        private static GymWriteOnlyRepositoryBuilder _instance;
        private readonly Mock<IGymWriteOnlyRepository> _repository;

        private GymWriteOnlyRepositoryBuilder()
        {
            if (_repository == null)
            {
                _repository = new Mock<IGymWriteOnlyRepository>();
            }
        }

        public static GymWriteOnlyRepositoryBuilder Instance()
        {
            _instance = new GymWriteOnlyRepositoryBuilder();
            return _instance;
        }

        public IGymWriteOnlyRepository Build()
        {
            return _repository.Object;
        }
    }
}
